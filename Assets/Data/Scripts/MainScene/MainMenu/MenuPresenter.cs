using UnityEngine;
using Zenject;

namespace Menu
{
    public class MenuPresenter
    {
        private MenuModel _model;
        private MenuView _view;
        
        [Inject]
        public void Init(MenuModel model, MenuView view)
        {
            _model = model;
            _view = view;

            _view.OnContinueAction += _model.Continue;
            _view.OnChooseLevelAction += _model.OpenLevelSelect;
            _view.OnOpenSettingsAction += _model.OpenSettings;
            _view.OnExitAction += _model.ExitGame;
        }

        public void Dispose()
        {
            _view.OnContinueAction -= _model.Continue;
            _view.OnChooseLevelAction -= _model.OpenLevelSelect;
            _view.OnOpenSettingsAction -= _model.OpenSettings;
            _view.OnExitAction -= _model.ExitGame;

            _view = null;
            _model = null;
        }
    }
}