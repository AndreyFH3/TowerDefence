using System.Collections.Generic;
using Levels.Info;
using UnityEngine;

namespace Levels.Enemies
{
    [CreateAssetMenu(fileName = "EnemiesData", menuName = "Game/Towers Data")]

    public class EnemyDataContainer : ScriptableObject
    {
        [SerializeField] private List<EnemyData> _enemies;

        public EnemyData GetEnemyData(string id) => _enemies.Find(el => el.EnemyId == id);
    }
}