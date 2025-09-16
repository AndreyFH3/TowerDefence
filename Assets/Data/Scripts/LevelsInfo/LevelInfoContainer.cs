using UnityEngine;
using System.Collections.Generic;

namespace Levels.Info
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
    public class LevelInfoContainer : ScriptableObject
    {
        [SerializeField] private List<LevelInfo> _levelsInfo;

        public int MaxLevelIndex => _levelsInfo.Count - 1;
        public LevelInfo[] LevelsInfo => _levelsInfo.ToArray();

        public int GetLevelNumber(string id) => _levelsInfo.IndexOf(GetLevelInfo(id));

        public LevelInfo GetLevelInfo(string id)
        {
            return _levelsInfo.Find(el => el.LevelId == id);
        }
        public LevelInfo GetLevelInfo(int id)
        {
            if (MaxLevelIndex >= id)
                return null;
            return _levelsInfo[id];
        }
    }
}