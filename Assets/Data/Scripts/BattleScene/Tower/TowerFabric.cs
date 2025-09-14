using UnityEngine;
using Levels.Tower;
using Zenject;
using Levels.Info.Tower;
using Levels.Managers;
using Levels.Info;

namespace Levels.Spawner
{
    public class TowerFabric : PlaceholderFactory<TowerModel>
    {
        private DiContainer _container;
        private TowerDataContainer _data;
        [Inject]
        public void Init(DiContainer container, TowerDataContainer data) 
        {
            _container = container;
            _data = data;
        }

        public TowerModel Create(Vector3 spawnPosition, BulletType type, BattleManager manager)
        {
            var data = _data.GetTowerData(type);
            TowerModel model = new TowerModel();
            TowerView view = _container.ResolveId<TowerView>(type.ToString());
            TowerPresenter presenter = new();

            model.Init(data.GetUpgrade(0), data.Type);
            model.SetPosition(spawnPosition);
            presenter.Init(model, view, manager);
            view.SetPosition(spawnPosition);

            return model;
        }
    }
}
