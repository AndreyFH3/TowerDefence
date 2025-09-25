using Levels.Info;
using Levels.Info.Tower;
using Levels.Managers;
using Levels.Tower;
using Sounds;
using UnityEngine;
using Zenject;

namespace Levels.Spawner
{
    public class TowerFactory : IFactory<Vector3, BulletType, BattleManager, TowerModel>
    {
        private DiContainer _container;
        private TowerDataContainer _data;
        private Zenject.SignalBus _signalBus;

        [Inject]
        public void Init(DiContainer container, TowerDataContainer data, Zenject.SignalBus signalBus) 
        {
            _container = container;
            _data = data;
            _signalBus = signalBus;
        }

        public TowerModel Create(Vector3 spawnPosition, BulletType type, BattleManager manager)
        {
            var data = _data.GetTowerData(type);

            TowerModel model = _container.Instantiate<TowerModel>();
            TowerView view = _container.ResolveId<TowerView>(type.ToString());
            TowerPresenter presenter = _container.Instantiate<TowerPresenter>();
            SoundPlayer soundPlayer = _container.Resolve<SoundPlayer>();

            model.Init(data.GetUpgrade(0), data.Type, _data);
            model.SetPosition(spawnPosition);
            
            presenter.Init(model,view,manager,_signalBus,soundPlayer);
            
            view.SetPosition(spawnPosition);

            return model;
        }
    }
}
