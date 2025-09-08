using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Core
{
    public class LoadingScreenPresenter
    {
        private LoadingScreenView _view;
        private LoadingScreenModel _model;

        [Inject]
        public void Init(LoadingScreenView view, LoadingScreenModel model)
        {
            _view = view;
            _model = model;

            _model.OnSceneLoadStart += EnableView;
            _model.OnSceneLoadFinish += DisableView;
            _model.OnSceneLoad += SetValue;
        }

        public void SetValue(float value)
        {
            _view.SetLoadingValue(value);
        } 
        public void Dispose() 
        {
            _model.OnSceneLoadStart -= EnableView;
            _model.OnSceneLoadFinish -= DisableView;
            _model.OnSceneLoad -= SetValue;

            _view = null;
            _model = null;
        }

        private void DisableView()
        {
            _view.Disable();
        }

        private void EnableView()
        {
            _view.Enable();
        }

        public void LoadBaseScene()
        {
            _model.LoadBaseSceneAsync();
        }

        public void LoadBootstrap()
        {
            _model.LoadBootstrapSceneAsync();
        }
    }
}