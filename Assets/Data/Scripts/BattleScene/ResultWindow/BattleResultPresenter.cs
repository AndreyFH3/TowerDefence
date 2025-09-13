using Levels.Managers;
using Zenject;

namespace Levels.Game
{

    public class BattleResultPresenter
    {
        private BattleManager _manager;
        private BattleResultView _view;

        [Inject]
        public void Init(BattleManager manger, BattleResultView view)
        {
            _manager = manger;
            _view = view;

            Subscribe();
            _view.gameObject.SetActive(false);
        }

        private void Subscribe()
        {
            _manager.OnBattleWin += SetWin;
            _manager.OnBattleLose += SetLose;
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