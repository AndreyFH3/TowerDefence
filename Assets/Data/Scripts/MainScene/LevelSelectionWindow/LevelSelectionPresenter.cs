using Zenject;

namespace Menu.LevelSelect
{
    public class LevelSelectionPresenter
    {
        private LevelSelectionView.Factory _viewFactory;
        private LevelSelectLevelInfo.Factory _viewInfoFactory;

        private LevelSelectionView _view;
        private LevelSelectionModel _model;


        [Inject]
        public void Init(LevelSelectionModel model, LevelSelectionView.Factory viewFactory, LevelSelectLevelInfo.Factory viewInfoFactory)
        {
            _model = model;
            _viewFactory = viewFactory;
            _viewInfoFactory = viewInfoFactory;
        }

        private void CreateLevelsInfo()
        {
            foreach (var info in _model.GetLevelsInfo())
            {
                var instance = _viewInfoFactory.Create();
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