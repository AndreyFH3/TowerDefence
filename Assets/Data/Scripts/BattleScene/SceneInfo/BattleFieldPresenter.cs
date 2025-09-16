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
		private DiContainer _container;
		private BattleManager _manager;
		private TowerSpotPresenter.Factory _factory;

        [Inject]
		public void Init(DiContainer container, LevelSceneInfo sceneInfo, BattleManager manager, TowerSpotPresenter.Factory factory)
		{ 
			_manager = manager;
			_container = container;
            _factory = factory;

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
					TowerSpotPresenter spotPresenter = _factory.Create();
					spotPresenter.Init(spotView);
				}
			}
		}
    }
}