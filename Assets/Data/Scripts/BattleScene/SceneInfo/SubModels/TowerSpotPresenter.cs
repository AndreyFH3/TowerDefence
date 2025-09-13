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

        public void Init(TowerSpotView view, TowerSpotModel model, SpotBuildSubView.Factory subBuildFactory, SpotBuildingSubUpgradeView.Factory subBuildUpgradeFactory)
        {
            _view = view;
            _model = model;
            _subBuildFactory = subBuildFactory;
            _subBuildUpgradeFactory = subBuildUpgradeFactory;

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
                view.OnTryBuyBase +=() => Debug.Log("base");
                view.OnTryBuyWater += () => Debug.Log("water");
                view.OnTryBuyFire += () => Debug.Log("fire");
            }
            else
            {
                var view = _subBuildUpgradeFactory.Create();
                view.OnUpgradeButtonClick += () => Debug.Log("upgrade");
                view.OnSellButtonClick += () => Debug.Log("sell");
            }
        }
    }
}