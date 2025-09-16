using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Levels.Info
{
    [System.Serializable]
    public class LevelInfo 
    {
        [SerializeField] private string _levelId;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private List<Wave> _waves = new();
        [SerializeField] private List<Vector3> _points;
        [SerializeField] public int _coinsDefault = 100;

        public string LevelId => _levelId;
        public Sprite Sprite => _sprite;
        public List<Wave> Waves => _waves;
        public List<Vector3> Points=> _points;
        public int CoinsDefault => _coinsDefault;

        public void SetPoints(Vector3[] points)
        {
            _points = points.ToList();
        }
    }

    [System.Serializable]
    public class Wave
    {
        [SerializeField] private List<string> _enemies = new();

        public List<string> Enemies => _enemies;
    }
}