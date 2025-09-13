using Levels.Managers;
using Levels.Tower;
using Zenject;

namespace Levels.Game.Sub
{
    public class TowerSpotModel
    {
        private TowerModel _tower;
        private SpotState _spotState;

        public TowerModel Tower => _tower;
        public SpotState SpotState => _spotState;

        [Inject]
        public void Init(BattleManager manager)
        {
            
        }

        public void SetTower (TowerModel model)
        {
            _spotState = SpotState.Set;
            _tower = model;
        }

        public void RemoveTower()
        {
            _spotState = SpotState.Empty;
            _tower = null;
        }
    }
}
