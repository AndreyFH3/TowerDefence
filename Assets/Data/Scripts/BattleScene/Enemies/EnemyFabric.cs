using UnityEngine;
using Zenject;
using Levels.Managers;
using Levels.Enemies;

namespace Levels.Spawner
{
    public class EnemyFabric : PlaceholderFactory<EnemyModel>
    {
        private DiContainer _container;
        private EnemyDataContainer _enemyDataContainer;
        private Zenject.SignalBus _signalBus;
        [Inject]
        public void Init(DiContainer container, EnemyDataContainer enemyDataContainer, Zenject.SignalBus signalBus)
        {
            _signalBus = signalBus;
            _container = container;
            _enemyDataContainer = enemyDataContainer;
        }

        public EnemyModel Create(Vector3 spawnPosition, string id, BattleManager manager)
        {
            var data = _enemyDataContainer.GetEnemyData(id);
            EnemyModel model = new EnemyModel();
            EnemyView view = _container.ResolveId<EnemyView>(data.EnemyType.ToString());
            EnemyPresenter presenter = new();

            model.Init(data, manager.Points);
            presenter.Init(model, view, _signalBus);
            view.SetPosition(spawnPosition);

            return model;
        }
    }
}
