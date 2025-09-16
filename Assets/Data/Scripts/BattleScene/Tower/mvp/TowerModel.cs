using System.ComponentModel;
using Levels.Enemies;
using Levels.Info;
using Levels.Info.Tower;
using UnityEngine;

namespace Levels.Tower
{
    public class TowerModel
    {
        private TowerData _towerData;
        private BulletType _bulletType;
        public int _level;

        public float AttackDelay {  get; protected set; }
        public float Damage {  get; protected set; }
        public float Distance {  get; protected set; }
        public int UpgradePrice { get; protected set; }
        public int SellPrice { get; protected set; }
        public Sprite Sprite { get; protected set; }

        public System.Action OnUpgrade;
        public System.Action<TowerModel> OnSell;

        public Vector3 Position { get; protected set; }

        public int Level => _level + 1;

        public bool IsMaxLevel => Level >= _towerData.MaxLevel;

        public void Init(TowerLevelInfo data, BulletType type, TowerDataContainer container)
        {
            SetData(data);
            _bulletType = type;
            _towerData = container.GetTowerData(type);
        }

        private void SetData(TowerLevelInfo data)
        {
            AttackDelay = data.AttackDelay;
            Damage = data.Damage;
            UpgradePrice = data.Price;
            Distance = data.Distance;
            Sprite = data.Sprite;
            SellPrice = data.SellPrice;
        }

        public void Upgrade()
        {
            if (IsMaxLevel) 
                return;

            var data = _towerData.GetUpgrade(Level);
            _level++;
            SetData(data);
            OnUpgrade?.Invoke();
        }

        public void Sell()
        {
            OnSell?.Invoke(this);
        }

        public void SetPosition(Vector3 pos) => Position = pos;

        public bool AttackEnemy(EnemyModel enemy)
        {
            if (Vector3.Distance(enemy.CurrentPosition, Position) <= Distance)
            {
                enemy.GetDamage(Damage, _bulletType);
                return true;
            }
            return false;
        }

    }
}
