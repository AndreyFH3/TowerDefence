using Game.Core;
using Levels;
using Levels.Info;
using UnityEngine;
using Zenject;

namespace Menu.LevelSelect
{
    public class LevelSelectionModel
    {
        private LevelInfoContainer _info;
        private LoadingScreenPresenter _loadingPresenter;
        private LevelSceneInfo _levelSceneInfo;

        public System.Action<string> OnClick;

        [Inject]
        public void Init(LevelInfoContainer info, LoadingScreenPresenter loadingPresenter, LevelSceneInfo levelSceneInfo)
        {
            _info = info;
            _loadingPresenter = loadingPresenter;
            _levelSceneInfo = levelSceneInfo;
        }

        public LevelInfo[] GetLevelsInfo()
        {
            return _info.LevelsInfo;
        }

        public void StartLevel(string levelId)
        {
            OnClick?.Invoke(levelId);
            _levelSceneInfo.LevelId = levelId;
            _loadingPresenter.LoadBattleScene();
        }
    }
}