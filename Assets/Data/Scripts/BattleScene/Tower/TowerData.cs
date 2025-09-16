using UnityEngine;
using System.Collections.Generic;

namespace Levels.Info.Tower
{
    [System.Serializable]
    public class TowerData
    {
        [SerializeField] private BulletType _type;
        [SerializeField] private int _buildPrice = 100;
        [SerializeField] private List<TowerLevelInfo> towerUpdateNext = null;
        public BulletType Type => _type;
        public int MaxLevel => towerUpdateNext.Count + 1;
        public int BuildPrice => _buildPrice;
        public TowerLevelInfo GetUpgrade(int index)
        {
            if (towerUpdateNext.Count < index)
                index = towerUpdateNext.Count - 1;
            else if (0 >= index)
                index = 0;

            return towerUpdateNext[index];
        }
    }

    [System.Serializable]
    public class TowerLevelInfo
    {
        [SerializeField] private float _attackDelay;
        [SerializeField] private float _damage;
        [SerializeField] private int _price;
        [SerializeField] private int _sellPrice;
        [SerializeField] private float _distance;
        [SerializeField] private Sprite _sprite;
        public float AttackDelay => _attackDelay;
        public float Damage => _damage;
        public float Distance => _distance;
        public int Price => _price;
        public int SellPrice => _sellPrice;
        public Sprite Sprite => _sprite;
    }
}
