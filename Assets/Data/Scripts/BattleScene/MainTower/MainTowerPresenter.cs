using System.Linq;
using Levels.Info;
using Zenject;

namespace Levels.Tower
{
    public class MainTowerPresenter
    {
        private MainTowerModel _model;
        private MainTowerView _view;
        [Inject]
        public void Init(MainTowerModel model, MainTowerView view, LevelInfoContainer infoContainer, LevelSceneInfo info)
        {
            _model = model;
            _view = view;

            Subscribe();

            _view.transform.position = infoContainer.GetLevelInfo(info.LevelId).Points.Last();
        }

        private void Subscribe()
        {
            _model.OnDie += UnSubscribe;
            _model.OnDie += _view.Die;
            _model.OnDamaged += _view.SetDamaged;
        }

        private void UnSubscribe()
        {
            _model.OnDie -= UnSubscribe;
            _model.OnDie -= _view.Die;
            _model.OnDamaged -= _view.SetDamaged;
        }
    }
}