using Core;
using Game.Core;
using Levels;
using PlayerData;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreenView _loadingScreenView;
        public override void InstallBindings()
        {
            #region LoadingScreen
            Container.Bind<LoadingScreenModel>().AsSingle().NonLazy();
            Container.Bind<LoadingScreenView>().FromComponentInNewPrefab(_loadingScreenView).AsSingle();
            Container.Bind<LoadingScreenPresenter>().AsSingle().NonLazy();
            Container.Bind<LevelSceneInfo>().AsSingle().NonLazy();
            #endregion
            Container.Bind<GameIniter>().AsSingle().NonLazy();

            #region Saves
            SaveSystem saveSystem = new SaveSystem();

            var progress = saveSystem.Get<CompanyProgress>("COMPANY_PROGRESS") ?? new CompanyProgress();
            Container.Bind<CompanyProgress>().FromInstance(progress).AsSingle();
            #endregion
        }
    }
}