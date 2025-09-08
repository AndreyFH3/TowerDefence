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

        public override void InstallBindings()
        {
            Container.BindInstance(_levelInfoContainer).AsSingle();
            Container.BindInstance(_towerDataContainer).AsSingle();
        }
    }
}