using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Levels.Info.Tower
{
    [System.Serializable]
    public class TowerData
    {
        [SerializeField] private BulletType _type;
        [SerializeField] private List<TowerLevelInfo> towerUpdateNext = null;

        public BulletType Type => _type;
        public TowerLevelInfo GetUpgrade(int index)
        {
            if (towerUpdateNext.Count < index)
                index = 0;
            else if (towerUpdateNext.Count >= index)
                index = towerUpdateNext.Count - 1;

            return towerUpdateNext[index];
        }
    }

    [System.Serializable]
    public class TowerLevelInfo
    {
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _damage;
        [SerializeField] private int _price;
        [SerializeField] private float _distance;
        [SerializeField] private Sprite _sprite;
        public float AttackDelay => _attackDelay;
        public float Damage => _damage;
        public float Distance => _distance;
        public int Price => _price;
        public Sprite Sprite => _sprite;
    }
}
