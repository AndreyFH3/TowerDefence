using System;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Levels.Enemies;
using Levels.Managers;

namespace Levels.Tower
{
    public class TowerPresenter
    {
        private TowerModel _model;
        private TowerView _view;
        private BattleManager _manager;

        private CancellationTokenSource _cts;

        public void Init(TowerModel model, TowerView view, BattleManager manager)
        {
            _model = model;
            _view = view;
            _manager = manager;

            Update(_cts.Token).Forget();
            Subscribe();
        }

        private async UniTask Update(CancellationToken ct)
        {
            try
            {
                while (true) 
                {
                    TryAttack(GetClosestEnemy());   
                    await UniTask.WaitForSeconds(_model.AttackDelay, true, PlayerLoopTiming.Update, ct);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Target Find Stop!");
            }
        }

        private void TryAttack(EnemyModel data)
        {
            if (data != null)
            {
                _view.Attack(data.CurrentPosition);
                _model.AttackEnemy(data);
            }
        }

        private EnemyModel GetClosestEnemy()
        {
            return _manager.GetClosestUnit(_view.transform.position);
        }

        private void Subscribe()
        {

        }

        private void Unsubscribe()
        {

        }

        public void Dispose()
        {
            _cts.Cancel();
            Unsubscribe();
        }
    }
}
