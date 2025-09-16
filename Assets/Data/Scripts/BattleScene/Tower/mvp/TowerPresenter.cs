using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Levels.Enemies;
using Levels.Managers;
using Levels.SignalBus;
using UnityEngine;
using Zenject;

namespace Levels.Tower
{
    public class TowerPresenter
    {
        private TowerModel _model;
        private TowerView _view;
        private BattleManager _manager;
        private Zenject.SignalBus _signalBus;

        private CancellationTokenSource _cts;
        private bool _isPaused = false;

        public void Init(TowerModel model, TowerView view, BattleManager manager, Zenject.SignalBus signalBus)
        {
            _model = model;
            _view = view;
            _manager = manager;
            _signalBus = signalBus;

            _signalBus.Subscribe<PauseBattleSignal>(SetPause);
            _signalBus.Subscribe<ResumeBattleSignal>(Continue);
            _model.OnUpgrade += OnLevelUpgrade;
            _model.OnSell += SellTower;
            _cts = new();
            Update(_cts.Token).Forget();
            OnLevelUpgrade();
        }

        public void OnLevelUpgrade()
        {
            _view.SetLevel(_model.Level);
        }

        private async UniTask Update(CancellationToken ct)
        {
            try
            {
                while (true) 
                {
                    if (_isPaused)
                    {
                        await UniTask.WaitForSeconds(_model.AttackDelay, true, PlayerLoopTiming.Update, ct);
                        continue;
                    }

                    TryAttack(GetClosestEnemy());   
                    await UniTask.WaitForSeconds(_model.AttackDelay, true, PlayerLoopTiming.Update, ct);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Target Find Stop!");
            }
        }

        private void SetPause() { _isPaused = true; }
        private void Continue() { _isPaused = false; }
        private void TryAttack(EnemyModel data)
        {
            if (data != null && _model.AttackEnemy(data))
            {
                 _view.Attack(data.CurrentPosition);
            }
        }

        private EnemyModel GetClosestEnemy()
        {
            return _manager.GetClosestUnit(_view.transform.position);
        }
        private void SellTower(TowerModel model)
        {
            Dispose();
        }
        public void Dispose()
        {
            _signalBus.Unsubscribe<PauseBattleSignal>(SetPause);
            _signalBus.Unsubscribe<ResumeBattleSignal>(Continue);
            _model.OnUpgrade -= OnLevelUpgrade;
            _model.OnSell -= SellTower;
            _cts.Cancel();
            GameObject.Destroy(_view.gameObject);
        }
    }
}
