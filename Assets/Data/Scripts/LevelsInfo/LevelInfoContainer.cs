using UnityEngine;
using System.Collections.Generic;

namespace Levels.Info
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/Level Data")]
    public class LevelInfoContainer : ScriptableObject
    {
        [SerializeField] private List<LevelInfo> _levelsInfo;

        public LevelInfo[] LevelsInfo => _levelsInfo.ToArray();

        public LevelInfo GetLevelInfo(string id)
        {
            return _levelsInfo.Find(el => el.LevelId == id);
        }


    }
}