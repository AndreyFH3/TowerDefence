using UnityEngine;
using UnityEngine.UI;

namespace Levels.Game
{
    public class PauseWindowView : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _homeButton;

        public System.Action OnResumeButtonClick;
        public System.Action OnHomeButtonClick;
        public System.Action OnDestroyObject;

        private void Awake()
        {
            _continueButton.onClick.AddListener(()=> { OnResumeButtonClick?.Invoke(); Close(); });
            _homeButton.onClick.AddListener(()=> OnHomeButtonClick?.Invoke());
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            OnDestroyObject?.Invoke();
        }
    }
}
