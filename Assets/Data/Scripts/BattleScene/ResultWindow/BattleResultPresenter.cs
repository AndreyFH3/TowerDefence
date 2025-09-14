using Game.Core;
using Levels.Managers;
using Zenject;
using UnityEngine;

namespace Levels.Game
{

    public class BattleResultPresenter
    {
        private BattleManager _manager;
        private BattleResultView _view;
        private LoadingScreenPresenter _loading;

        [Inject]
        public void Init(BattleManager manger, BattleResultView view, LoadingScreenPresenter loading)
        {
            _manager = manger;
            _view = view;
            _loading = loading;

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
            Debug.Log("Loaded prev level!");
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
            _view.gameObject.SetActive(true);
            _view.SetResults(true);
        }
    }
}