using Game.Core;
using Levels.Managers;
using Zenject;
using UnityEngine;
using Levels.Info;

namespace Levels.Game
{

    public class BattleResultPresenter
    {
        private BattleManager _manager;
        private BattleResultView _view;
        private LoadingScreenPresenter _loading;
        private LevelInfoContainer _levelContainer;
        private string _levelId;

        [Inject]
        public void Init(BattleManager manger, BattleResultView view, LoadingScreenPresenter loading, LevelInfoContainer levelContainer, LevelSceneInfo sceneInfo)
        {
            _manager = manger;
            _view = view;
            _loading = loading;
            _levelContainer = levelContainer;
            _levelId = sceneInfo.LevelId;

            Subscribe();
            _view.gameObject.SetActive(false);
        }

        private void Subscribe()
        {
            _manager.OnBattleWin += SetWin;
            _manager.OnBattleLose += SetLose;
            _view.OnHomeButtonClick += LoadHome;
            _view.OnNextButtonClick += NextMission;
        }

        private void NextMission()
        {
            _levelContainer.GetLevelInfo(_levelContainer.GetLevelNumber(_levelId) + 1);
            _loading.LoadBattleScene();
        }

        private void LoadHome()
        {
            _loading.LoadBaseScene();
        }


        private void SetLose()
        {
            _view.gameObject.SetActive(true);
            _view.SetResults(false);
        }

        private void SetWin()
        {
            if (_levelContainer.GetLevelNumber(_levelId) + 1 >= _levelContainer.MaxLevelIndex)
                _view.DisableNextMissionButton();

            _view.gameObject.SetActive(true);
            _view.SetResults(true);
        }
    }
}