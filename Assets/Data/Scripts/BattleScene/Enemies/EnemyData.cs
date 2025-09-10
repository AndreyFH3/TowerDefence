using UnityEngine;

namespace Levels.Info
{
    [System.Serializable]
    public class EnemyData
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private string _enemyId;
        [SerializeField] private string _enemyName;
        [SerializeField] private int _health;
        [SerializeField] private int _attackPower;
        [SerializeField] private float _speed;

        public EnemyType EnemyType =>_enemyType;
        public string EnemyId =>_enemyId;
        public string EnemyName => _enemyName;
        public int Health => _health;
        public int AttackPower =>_attackPower;
        public float Speed =>_speed;
    }
}