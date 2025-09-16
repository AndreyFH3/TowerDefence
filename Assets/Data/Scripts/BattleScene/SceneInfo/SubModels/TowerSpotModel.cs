using Levels.Info;
using Levels.Info.Tower;
using Levels.Tower;
using Zenject;

namespace Levels.Game.Sub
{
    public class TowerSpotModel
    {
        private TowerModel _tower;
        private SpotState _spotState;
        private Wallet _wallet;
        private TowerDataContainer _towerContainer;
        public TowerModel Tower => _tower;
        public SpotState SpotState => _spotState;

        [Inject]
        public void Init(Wallet wallet, TowerDataContainer towerContainer)
        {
            _wallet = wallet;
            _towerContainer = towerContainer;
        }

        public bool CheckTowerPrice(BulletType type)
        {
            var price = GetPrice(type);
            return _wallet.CheckEnough(price);
        }

        public bool CheckTowerUpgrade()
        {
            if (Tower is null) 
                return false;
            
            return _wallet.CheckEnough(Tower.UpgradePrice);
        }

        public int GetPrice(BulletType type)
        {
            var price = _towerContainer.GetTowerData(type).BuildPrice;

            return price;
        }

        public bool TryBuyTower(BulletType type)
        {
            if (_wallet.TrySpend(GetPrice(type)))
                return true;
            
            return false;
        }

        public void TryUpgradeTower()
        {
            if(Tower == null || !_wallet.TrySpend(Tower.UpgradePrice))
                return;

            Tower.Upgrade();
        }

        public void SetTower (TowerModel model)
        {
            
            if (model is null)
            {
                SellTower();
                return;
            }
            _tower = model;
            _spotState = SpotState.Set;
        }

        public void SellTower()
        {
            if(Tower is null)
                return;

            _wallet.AddCoins(Tower.SellPrice);
            Tower.Sell();
            RemoveTower();
        }

        private void RemoveTower()
        {

            _spotState = SpotState.Empty;
            _tower = null;
        }
    }
}
