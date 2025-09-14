using Levels.Enemies;
using Levels.Info;
using Levels.Info.Tower;
using Unity.VisualScripting;
using UnityEngine;

namespace Levels.Tower
{
    public class TowerModel
    {
        private BulletType _bulletType;
        public float AttackDelay {  get; protected set; }
        public float Damage {  get; protected set; }
        public float Distance {  get; protected set; }
        public int Price { get; protected set; }
        public Sprite Sprite { get; protected set; }

        public System.Action OnUpgrade;

        public Vector3 Position { get; protected set; }

        public int Level { get; protected set; }

        public void Init(TowerLevelInfo data, BulletType type)
        {
            _bulletType = type;
            AttackDelay = data.AttackDelay;
            Damage = data.Damage;
            Price = data.Price;
            Distance = data.Distance;
            Sprite = data.Sprite;
        }

        public void Upgrade()
        {
            Level++;
            OnUpgrade?.Invoke();
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
