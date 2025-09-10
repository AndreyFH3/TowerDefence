using Levels.Enemies;
using Levels.Info;
using Levels.Info.Tower;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Installers/Level Repository Installer")]
    public class GlobalConfigInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LevelInfoContainer _levelInfoContainer;
        [SerializeField] private TowerDataContainer _towerDataContainer;
        [SerializeField] private EnemyDataContainer _enemyDataContainer;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelInfoContainer).AsSingle();
            Container.BindInstance(_towerDataContainer).AsSingle();
            Container.BindInstance(_enemyDataContainer).AsSingle();
        }
    }
}