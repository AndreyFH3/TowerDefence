using Game.Core;
using Levels;
using Levels.Info;
using Menu.LevelSelect;
using PlayerData;
using Sounds;
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
        private SoundPlayer _soundPlayer;

        [Inject]
        public void Init(LevelSelectionPresenter presenter, CompanyProgress progress, LevelSceneInfo levelSceneInfo, LevelInfoContainer info, LoadingScreenPresenter loadingPresenter, SoundPlayer soundPlayer)
        {
            _loadingPresenter = loadingPresenter;
            _levelSelectionPresenter = presenter;
            _progress = progress;
            _levelSceneInfo = levelSceneInfo;
            _info = info;
            _soundPlayer = soundPlayer;
            _soundPlayer.PlayMainSceneMusic();
        }

        public void Continue()
        {
            var level = _info.GetLevelInfo(_progress.LastPassed);
            if (level == null)
                return;
            _levelSceneInfo.LevelId = level.LevelId;
            _loadingPresenter.LoadBattleScene();
            _soundPlayer.PlayClickSound();
        }

        public void OpenLevelSelect()
        {
            _soundPlayer.PlayClickSound();
            _levelSelectionPresenter.CreateWindow();
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            _soundPlayer.PlayClickSound();
#else
            _soundPlayer.PlayClickSound();
            Application.Quit();
#endif
        }
    }
}