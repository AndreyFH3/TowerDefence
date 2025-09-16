using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Levels.Info;
using Levels.SignalBus;
using UnityEngine;
using Zenject;

namespace Levels.Enemies
{
    public class EnemyPresenter
    {
        private EnemyModel _enemyModel;
        private EnemyView _enemyView;
        private Zenject.SignalBus _signalBus;
        private CancellationTokenSource _cts;

        private bool _isPaused = false;
        
        public void Init(EnemyModel model, EnemyView view, Zenject.SignalBus signalBus)
        {
            _enemyModel = model;
            _enemyView = view;
            _signalBus = signalBus;

            _cts = new();
            Subscribe();

            Update(_cts.Token).Forget();
            _enemyView.SetHealth(1);
        }

        private async UniTask Update(CancellationToken cts)
        {
            try
            {
                _enemyView.SetWalkingAnimation();
                while (_enemyModel.IsAlive)
                {
                    if (_isPaused)
                    {
                        await UniTask.WaitForFixedUpdate(cts);
                        continue;
                    }

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
            _signalBus.Subscribe<PauseBattleSignal>(SetPause);
            _signalBus.Subscribe<ResumeBattleSignal>(Continue);
            _enemyModel.OnDie += Die;
            _enemyModel.OnUnitDamaged += _enemyView.SetHealth;
            _enemyModel.OnDamageMainTower += OnTowerAchieve;
            _enemyView.OnDestroyGameObject += Unsubscribe;
        }

        private void SetPause() { _isPaused = true; }
        private void Continue() { _isPaused = false; }

        public void Unsubscribe()
        {
            _signalBus.Unsubscribe<PauseBattleSignal>(SetPause);
            _signalBus.Unsubscribe<ResumeBattleSignal>(Continue);
            _enemyModel.OnDie -= Die;
            _enemyModel.OnUnitDamaged -= _enemyView.SetHealth;
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
            _cts?.Cancel();
            GameObject.Destroy(_enemyView.gameObject);
        }
    }
}