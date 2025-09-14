using Levels.Game.Sub;
using Levels.Info;
using Levels.Managers;
using UnityEngine;
using Zenject;

namespace Levels.Game
{
	public class BattleFieldPresenter
	{
		private BattleFieldView _view;
		private BattleManager _manager;
		private DiContainer _container;
        private SpotBuildSubView.Factory _subBuildFactory;
        private SpotBuildingSubUpgradeView.Factory _subBuildUpgradeFactory;

        [Inject]
		public void Init(BattleManager manager, DiContainer container, LevelSceneInfo sceneInfo, SpotBuildSubView.Factory subBuildFactory, SpotBuildingSubUpgradeView.Factory subBuildUpgradeFactory)
		{ 
			_manager = manager;
			_container = container;
			_subBuildFactory = subBuildFactory;
			_subBuildUpgradeFactory = subBuildUpgradeFactory;

            CreateBattleFieldView(sceneInfo.LevelId);
			InitSpots();      
		}

		private void CreateBattleFieldView(string id)
		{
			if(!string.IsNullOrEmpty(id))
				_view = _container.ResolveId<BattleFieldView.BattleFieldViewFabric>(id).Create();
		}

		private void InitSpots()
		{
			if(_view != null)
			{
				foreach (var spotView in _view.Spots)
				{
					TowerSpotModel model = new();
					TowerSpotPresenter spotPresenter = new TowerSpotPresenter();
					spotPresenter.Init(spotView, model, _subBuildFactory, _subBuildUpgradeFactory, _manager);
				}
			}
		}
    }
}