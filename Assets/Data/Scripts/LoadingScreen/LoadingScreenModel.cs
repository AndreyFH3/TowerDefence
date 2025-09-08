using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Core
{
    public class LoadingScreenModel
    {
        private ZenjectSceneLoader _sceneLoader;
        private enum ScenesNames { MenuScene, BootstrapScene }
    
        public System.Action OnSceneLoadStart;
        public System.Action OnSceneLoadFinish;
        public System.Action<float> OnSceneLoad;
        [Inject]
        public void Init(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void LoadSceneAsync(ScenesNames sceneName)
        {
            string sceneId = sceneName.ToString();
            OnSceneLoadStart?.Invoke();
            LoadScene(sceneId).Forget();
        }

        private async UniTask LoadScene(string sceneId)
        {
            try
            {
                AsyncOperation result = _sceneLoader.LoadSceneAsync(sceneId);
                while (!result.isDone)
                {
                    OnSceneLoad?.Invoke(result.progress);
                    await UniTask.WaitForEndOfFrame();
                }
                OnSceneLoadFinish?.Invoke();
            }
            catch 
            {
                Debug.Log("Scene Loading Canceled!");
            }
        }
        public void LoadBootstrapSceneAsync()
        {
            LoadSceneAsync(ScenesNames.BootstrapScene);
        }

        public void LoadBaseSceneAsync()
        {
            LoadSceneAsync(ScenesNames.MenuScene);
        }
    }
}