using UnityEngine;
using Levels.Tower;
using Zenject;
using Levels.Info.Tower;
using Levels.Managers;

namespace Levels.Spawner
{
    public class TowerFabric : PlaceholderFactory<Vector3, TowerData>
    {
        private DiContainer _container;
        [Inject]
        public void Init(DiContainer container) 
        {
            _container = container;
        }

        public TowerModel Create(Vector3 spawnPosition, TowerData data, BattleManager manager)
        {
            TowerModel model = new TowerModel();
            TowerView view = _container.ResolveId<TowerView>(data.Type.ToString());
            TowerPresenter presenter = new();

            model.Init(data.GetUpgrade(0), data.Type);
            presenter.Init(model, view, manager);
            view.SetPosition(spawnPosition);

            return model;
        }
    }
}
