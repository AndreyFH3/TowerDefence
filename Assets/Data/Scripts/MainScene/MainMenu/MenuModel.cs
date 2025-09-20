using Game.Core;
using Levels;
using Levels.Info;
using Menu.LevelSelect;
using PlayerData;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Menu
{
    public class MenuModel 
    {
        private LevelSelectionPresenter _levelSelectionPresenter;
        private CompanyProgress _progress;
        private LevelSceneInfo _levelSceneInfo;
        private LevelInfoContainer _info;
        private LoadingScreenPresenter _loadingPresenter;

        [Inject]
        public void Init(LevelSelectionPresenter presenter, CompanyProgress progress, LevelSceneInfo levelSceneInfo, LevelInfoContainer info, LoadingScreenPresenter loadingPresenter)
        {
            _loadingPresenter = loadingPresenter;
            _levelSelectionPresenter = presenter;
            _progress = progress;
            _levelSceneInfo = levelSceneInfo;
            _info = info;
        }

        public void Continue()
        {
            var level = _info.GetLevelInfo(_progress.LastPassed);
            if (level == null)
                return;
            _levelSceneInfo.LevelId = level.LevelId;
            _loadingPresenter.LoadBattleScene();
        }

        public void OpenLevelSelect()
        {
            _levelSelectionPresenter.CreateWindow();
        }

        public void OpenSettings()
        {
            Debug.Log("OpenSettings");
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}