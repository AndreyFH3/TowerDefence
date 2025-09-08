using UnityEngine;
using Levels.Info.Tower;
using Levels.Enemies;
using Levels.Info;

namespace Levels.Tower
{
    public class TowerModel
    {
        private BulletType _bulletType;
        public float AttackDelay {  get; protected set; }
        public float Damage {  get; protected set; }
        public int Price { get; protected set; }
        public Sprite Sprite { get; protected set; }

        public System.Action OnUpgrade;

        public int Level { get; protected set; }

        public void Init(TowerLevelInfo data, BulletType type)
        {
            _bulletType = type;
            AttackDelay = data.AttackDelay;
            Damage = data.Damage;
            Price = data.Price;
            Sprite = data.Sprite;
        }

        public void Upgrade()
        {
            Level++;
            OnUpgrade?.Invoke();
        }

        public void AttackEnemy(EnemyModel enemy)
        {
            enemy.GetDamage(Damage, _bulletType);
        }

    }
}
