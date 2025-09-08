using Levels.Info;
using UnityEngine;
using Zenject;

namespace Menu.LevelSelect
{
    public class LevelSelectionModel
    {
        private LevelInfoContainer _info;
        [Inject]
        public void Init(LevelInfoContainer info)
        {
            _info = info;
        }

        public LevelInfo[] GetLevelsInfo()
        {
            return _info.LevelsInfo;
        }

        public void StartLevel(string levelId)
        {
            Debug.Log(levelId);
        }
    }
}