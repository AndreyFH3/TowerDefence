using Game.Core;
using Levels.Managers;
using Levels.SignalBus;
using Zenject;

namespace Levels.Game
{
    public class PauseWindowPresenter
    {
        private BattleManager _manager;
        private PauseWindowView _view;
        private Zenject.SignalBus _signalBus;
        private LoadingScreenPresenter _loadingScreen;
        [Inject]
        public void Init(BattleManager manager, PauseWindowView view, Zenject.SignalBus signalBus, LoadingScreenPresenter loadingScreen)
        {
            _manager = manager;
            _view = view;
            _signalBus = signalBus;
            _loadingScreen = loadingScreen;

            Subscribe();
            _view.gameObject.SetActive(false);
        }

        private void Subscribe()
        {
            _view.OnResumeButtonClick += Resume;
            _view.OnHomeButtonClick += GoHome;
            _view.OnDestroyObject += Unsubscribe;

            _signalBus.Subscribe<PauseBattleSignal>(ActivateView);
        }

        private void ActivateView()
        {
            _view.gameObject.SetActive(true);
        }

        private void GoHome()
        {
            _loadingScreen.LoadBaseScene();
        }

        private void Resume()
        {
            _manager.Resume();
        }

        private void Unsubscribe()
        {

            _view.OnResumeButtonClick -= Resume;
            _view.OnHomeButtonClick -= GoHome;
            _view.OnDestroyObject -= Unsubscribe;

            _signalBus.Unsubscribe<PauseBattleSignal>(ActivateView);
        }
    }
}