using System.Collections.Generic;
using Levels.Info;
using UnityEngine;
using Zenject;

namespace Levels.Enemies
{
    public class EnemyModel
    {
        private Vector3[] _cells;
        
        private float _currentHealth;
        private int _maxHealth;
        private int _cellIndex;
        private readonly Dictionary<(EnemyType, BulletType), float> _multipliers =
            new()
            {
                {(EnemyType.Base, BulletType.Base), 1f},
                {(EnemyType.Base, BulletType.Water), 0.8f},
                {(EnemyType.Base, BulletType.Fire), 0.8f},

                {(EnemyType.Water, BulletType.Base), 0.9f},
                {(EnemyType.Water, BulletType.Water), 0.25f},
                {(EnemyType.Water, BulletType.Fire), 2f},

                {(EnemyType.Fire, BulletType.Base), 0.9f},
                {(EnemyType.Fire, BulletType.Fire), 0.25f},
                {(EnemyType.Fire, BulletType.Water), 2f},
            };
        public bool IsAlive { get; protected set; } = true;
        public float Speed { get; protected set; }
        public float Damage { get; protected set; }
        public string Name { get; protected set; }
        public int KillCost { get; protected set; }
        private EnemyType _enemyType;

        public Vector3 CurrentPosition { get; set; }
        public float HealthPercents => _currentHealth / _maxHealth;

        public System.Action<EnemyModel> OnDie;
        public System.Action<EnemyModel> OnDamageMainTower;        
        public System.Action<float> OnUnitDamaged; 

        [Inject]        
        public void Init(EnemyData config, Vector3[] way)
        {
            _currentHealth = config.Health;
            _maxHealth = config.Health;
            _cells = way;
            Name = config.EnemyName;
            Speed = config.Speed;
            Damage = config.AttackPower;
            KillCost = config.KillCost;
        }

        public void GetDamage(float value, BulletType bulletType)
        {
            var damageValue = value * CalculateMultiplayer(bulletType);
            _currentHealth -= damageValue;
            OnUnitDamaged?.Invoke(HealthPercents);
            if(_currentHealth <= 0)
            {
                IsAlive = false;
                OnDie?.Invoke(this);
            }
        }

        public Vector3 GetCurrentCell()
        {
            return _cells[_cellIndex];
        }

        public void NextCell()
        {
            _cellIndex++;
            if(_cellIndex >= _cells.Length)
            {
                OnDamageMainTower?.Invoke(this);
                _cellIndex = _cells.Length - 1;
            }
        }

        private float CalculateMultiplayer(BulletType bulletType)
        {
            return _multipliers.TryGetValue((_enemyType, bulletType), out var value) ? value : 1f;
        }
    }
}