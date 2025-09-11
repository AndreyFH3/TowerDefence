using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Levels.Info;
using UnityEditor;
using UnityEngine;

namespace Levels.Enemies
{
    public class EnemyPresenter
    {
        private EnemyModel _enemyModel;
        private EnemyView _enemyView;

        private CancellationTokenSource _cts;
        
        public void Init(EnemyModel model, EnemyView view)
        {
            _enemyModel = model;
            _enemyView = view;

            _cts = new();
            Subscribe();

            Update(_cts.Token).Forget();
        }

        private async UniTask Update(CancellationToken cts)
        {
            try
            {
                _enemyView.SetWalkingAnimation();
                while (_enemyModel.IsAlive)
                {
                    Vector3 currentPosition = _enemyView.transform.position;
                    Vector3 target = _enemyModel.GetCurrentCell();

                    Vector3 nextPosition = Vector3.MoveTowards(currentPosition, target, Time.fixedDeltaTime * _enemyModel.Speed);
                    _enemyView.SetPosition(nextPosition);
                    _enemyModel.CurrentPosition = nextPosition;
                    if (Vector3.Distance(nextPosition, target) <= 0.1f)
                        _enemyModel.NextCell();

                    await UniTask.WaitForFixedUpdate(cts);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning("Move is canceled!");
            }
        }

        private void Subscribe()
        {
            _enemyModel.OnDie += Die;
            _enemyModel.OnDamageMainTower += OnTowerAchieve;
            _enemyView.OnDestroyGameObject += Unsubscribe;
        }

        public void Unsubscribe()
        {
            _enemyModel.OnDie -= Die;
            _enemyModel.OnDamageMainTower -= OnTowerAchieve;
            _enemyView.OnDestroyGameObject -= Unsubscribe;
        }

        private void Die(EnemyModel model)
        {
            if (_enemyModel.IsAlive)
                return;
            
            _enemyView.SetDieAnimation();
        }

        private void OnTowerAchieve(EnemyModel model)
        {
            _cts.Cancel();
            Die(model);
        }
    }
}