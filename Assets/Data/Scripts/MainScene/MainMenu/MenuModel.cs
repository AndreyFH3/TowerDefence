using Levels.Info;
using Menu.LevelSelect;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Menu
{
    public class MenuModel 
    {
        private LevelSelectionPresenter _levelSelectionPresenter;

        [Inject]
        public void Init(LevelSelectionPresenter presenter)
        {
            _levelSelectionPresenter = presenter;
        }

        public void Continue()
        {
            Debug.Log("Continue");
        }

        public void OpenLevelSelect()
        {
            _levelSelectionPresenter.CreateWindow();
        }

        public void OpenSettings()
        {
            Debug.Log("OpenSettings");
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}