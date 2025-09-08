using System.Collections.Generic;
using UnityEngine;

namespace Levels.Info
{
    [System.Serializable]
    public class LevelInfo 
    {
        [SerializeField] private string _levelId;
        [SerializeField] private Sprite _sprite;
        private List<List<EnemyData>> _waves = new();

        public string LevelId => _levelId;
        public Sprite Sprite => _sprite;
        public List<EnemyData>[] Waves;
    }
}