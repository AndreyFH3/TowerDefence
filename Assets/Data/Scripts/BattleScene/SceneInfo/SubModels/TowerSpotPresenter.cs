using Levels.Info;
using Levels.Managers;
using UnityEngine;

namespace Levels.Game.Sub
{
    public class TowerSpotPresenter
    {
        private TowerSpotView _view;
        private TowerSpotModel _model;

        private SpotBuildSubView.Factory _subBuildFactory;
        private SpotBuildingSubUpgradeView.Factory _subBuildUpgradeFactory;
        private BattleManager _manager;   
        public void Init(TowerSpotView view, TowerSpotModel model, SpotBuildSubView.Factory subBuildFactory, SpotBuildingSubUpgradeView.Factory subBuildUpgradeFactory, BattleManager manager)
        {
            _view = view;
            _model = model;
            _subBuildFactory = subBuildFactory;
            _subBuildUpgradeFactory = subBuildUpgradeFactory;
            _manager = manager;
            Subscribe();
        }

        public void Subscribe()
        {
            _view.OnClick += SetClickUI;
        }

        public void SetClickUI(Vector3 position)
        {
            if (_model.SpotState == SpotState.Empty)
            { 
                var view = _subBuildFactory.Create();
                view.transform.position = position;
                view.OnTryBuyBase +=() => CreateTower(position, BulletType.Base);
                view.OnTryBuyWater += () => CreateTower(position, BulletType.Water);
                view.OnTryBuyFire += () => CreateTower(position, BulletType.Fire);
            }
            else
            {
                var view = _subBuildUpgradeFactory.Create();
                view.transform.position = position;
                view.OnUpgradeButtonClick += () => _model.Tower.Upgrade() ;
                view.OnSellButtonClick += () => _model.RemoveTower();
            }
        }

        private void CreateTower(Vector3 pos, BulletType type)
        {
            var tower = _manager.AddTower(pos, type);
            _model.SetTower(tower);
        }
    }
}