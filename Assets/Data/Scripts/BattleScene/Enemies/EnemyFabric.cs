using UnityEngine;
using Levels.Tower;
using Zenject;
using Levels.Info.Tower;
using Levels.Managers;
using Levels.Info;
using Levels.Enemies;

namespace Levels.Spawner
{
    public class EnemyFabric : PlaceholderFactory<Vector3, EnemyData>
    {
        private DiContainer _container;
        [Inject]
        public void Init(DiContainer container)
        {
            _container = container;
        }

        public EnemyModel Create(Vector3 spawnPosition, EnemyData data, BattleManager manager)
        {
            EnemyModel model = new EnemyModel();
            EnemyView view = _container.ResolveId<EnemyView>(data.EnemyType.ToString());
            EnemyPresenter presenter = new();

            model.Init(data, manager.Cells);
            presenter.Init(model, view);
            view.SetPosition(spawnPosition);

            return model;
        }
    }
}
