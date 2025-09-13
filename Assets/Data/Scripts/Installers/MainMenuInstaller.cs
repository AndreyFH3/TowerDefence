using Menu;
using Menu.LevelSelect;
using Zenject;
using UnityEngine;

namespace Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private LevelSelectionView _levelSelectionView;
        [SerializeField] private LevelSelectLevelInfo _levelSelectLevelInfo;
        public override void InstallBindings()
        {
            #region Menu
            Container.Bind<MenuModel>().AsSingle();
            Container.Bind<MenuView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MenuPresenter>().AsSingle().NonLazy();
            #endregion

            #region Level Select
            Container.Bind<LevelSelectionModel>().AsSingle();
            Container.BindFactory<LevelSelectionView, LevelSelectionView.Factory>().FromComponentInNewPrefab(_levelSelectionView);
            Container.Bind<LevelSelectionPresenter>().AsSingle().NonLazy();

            Container.BindFactory<LevelSelectLevelInfo, LevelSelectLevelInfo.Factory>().FromComponentInNewPrefab(_levelSelectLevelInfo);
            #endregion
        }
    }
}