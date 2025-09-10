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
		
		[Inject]
		public void Init(BattleManager manager, DiContainer container, LevelSceneInfo sceneInfo)
		{ 
			_manager = manager;
			_container = container;

            CreateBattleFieldView(sceneInfo.LevelId);
			Subscribe();      
		}

		private void CreateBattleFieldView(string id)
		{
			if(!string.IsNullOrEmpty(id))
				_view = _container.ResolveId<BattleFieldView.BattleFieldViewFabric>(id).Create();
		}

		private void Subscribe()
		{
			if(_view != null)
	            System.Array.ForEach(_view.Spots, el => el.OnClick += SetBuildTowerView);
		}

		private void SetBuildTowerView(Vector3 position)
		{

		}
    }
}