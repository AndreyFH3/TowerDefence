using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Levels.Info;
using Levels.Managers;
using UnityEngine;
using Zenject;

namespace Levels.Game.Sub
{
    public class TowerSpotPresenter
    {
        private TowerSpotView _view;
        private TowerSpotModel _model;

        private SpotBuildSubView.Factory _subBuildFactory;
        private SpotBuildingSubUpgradeView.Factory _subBuildUpgradeFactory;
        private BattleManager _manager;
        private CancellationTokenSource _cts;
        
        [Inject]
        public void Init(TowerSpotModel model, SpotBuildSubView.Factory subBuildFactory, SpotBuildingSubUpgradeView.Factory subBuildUpgradeFactory, BattleManager manager)
        {
            _model = model;
            _subBuildFactory = subBuildFactory;
            _subBuildUpgradeFactory = subBuildUpgradeFactory;
            _manager = manager;
        }
        
        public void Init(TowerSpotView view)
        {
            _view = view;
            Subscribe();
        }

        public void Subscribe()
        {
            _view.OnClick += SetClickUI;
        }

        public void SetClickUI(Vector3 position)
        {
            _cts = new();
            if (_model.SpotState == SpotState.Empty)
            { 
                var view = _subBuildFactory.Create();
                view.transform.position = position;
                
                view.SetBasePrice(_model.GetPrice(BulletType.Base));
                view.SetFirePrice(_model.GetPrice(BulletType.Fire));
                view.SetWaterPrice(_model.GetPrice(BulletType.Water));

                view.OnTryBuyBase +=() => CreateTower(position, BulletType.Base);
                view.OnTryBuyWater += () => CreateTower(position, BulletType.Water);
                view.OnTryBuyFire += () => CreateTower(position, BulletType.Fire);
                
                CheckTowerAvailable(view, _cts.Token).Forget();
            }
            else
            {
                var view = _subBuildUpgradeFactory.Create();
                view.transform.position = position;
                view.SetUpgradeButtonInteractable(_model.CheckTowerUpgrade());
                view.DisableUpgradeButton(!_model.Tower.IsMaxLevel);
                view.SetPrice(_model.Tower.UpgradePrice);
                view.OnUpgradeButtonClick += TryUpgradeTower;
                view.OnSellButtonClick += TrySell;
                CheckTowerAvailable(view, _cts.Token).Forget();
            }
        }

        private void TrySell()
        {
            _model.SellTower();
        }

        private async UniTask CheckTowerAvailable(SpotBuildingSubUpgradeView view, CancellationToken token)
        {
            try
            {
                while (view != null)
                {
                    view.SetUpgradeButtonInteractable(_model.CheckTowerUpgrade());
                    view.DisableUpgradeButton(!_model.Tower.IsMaxLevel);
                    view.SetPrice(_model.Tower.UpgradePrice);
                    await UniTask.WaitForEndOfFrame();
                }
            }
            catch (OperationCanceledException) { }
        }

        private void TryUpgradeTower()
        {
            if (_model.CheckTowerUpgrade()) 
                _model.TryUpgradeTower();
        }

        private async UniTask CheckTowerAvailable(SpotBuildSubView view, CancellationToken token)
        {
            try
            {
                while(view != null)
                {
                    view.SetBaseButtonInteraction(_model.CheckTowerPrice(BulletType.Base));
                    view.SetFireButtonInteraction(_model.CheckTowerPrice(BulletType.Fire));
                    view.SetWaterButtonInteraction(_model.CheckTowerPrice(BulletType.Water));
                    await UniTask.WaitForEndOfFrame();
                }
            }
            catch (OperationCanceledException) { }
        }

        private void CreateTower(Vector3 pos, BulletType type)
        {
            if (!_model.TryBuyTower(type))
                return;
             
            var tower = _manager.AddTower(pos, type);
            _model.SetTower(tower);
        }

        public class Factory : PlaceholderFactory<TowerSpotPresenter> {  }
    }
}