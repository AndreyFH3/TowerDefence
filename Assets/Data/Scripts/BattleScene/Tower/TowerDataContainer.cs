using System.Collections.Generic;
using UnityEngine;

namespace Levels.Info.Tower
{
    [CreateAssetMenu(fileName = "TowersData", menuName = "Game/Towers Data")]
    public class TowerDataContainer : ScriptableObject
    {
        [SerializeField] public List<TowerData> _towerDatas;

        public TowerData GetTowerData(BulletType type)
        {
            return _towerDatas.Find(el => el.Type == type);
        }
    }
}
