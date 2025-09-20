using PlayerData;
using UnityEngine;
using Zenject;

namespace Menu.LevelSelect
{
    public class LevelSelectionPresenter
    {
        private LevelSelectionView.Factory _viewFactory;
        private LevelSelectLevelInfo.Factory _viewInfoFactory;

        private LevelSelectionView _view;
        private LevelSelectionModel _model;
        private CompanyProgress _progress;


        [Inject]
        public void Init(LevelSelectionModel model, LevelSelectionView.Factory viewFactory, LevelSelectLevelInfo.Factory viewInfoFactory, CompanyProgress progress)
        {
            _model = model;
            _viewFactory = viewFactory;
            _viewInfoFactory = viewInfoFactory;
            _progress = progress;
        }

        private void CreateLevelsInfo()
        {
            for (int i = 0; i < _model.GetLevelsInfo().Length; i++)
            {
                var info = _model.GetLevelsInfo()[i];

                var instance = _viewInfoFactory.Create();
                if (i > _progress.LastPassed)
                    instance.DisableClick();
                instance.SetData(info);
                instance.OnClick += () => { OnLevelSelect(info.LevelId); };
                _view.SetParent(instance.transform);
            }
        }

        private void OnLevelSelect(string levelId)
        {
            _model.StartLevel(levelId);
        }

        public void CreateWindow()
        {
            _view = _viewFactory.Create();
            CreateLevelsInfo();
        }

        public void Dispose() { }

    }
}