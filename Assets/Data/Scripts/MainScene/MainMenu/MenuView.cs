using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button chooseLevelButton;
        [SerializeField] private Button exitButton;

        public System.Action OnContinueAction;
        public System.Action OnChooseLevelAction;
        public System.Action OnExitAction;

        private void Awake()
        {
            continueButton.onClick.AddListener(ContinueAction);
            chooseLevelButton.onClick.AddListener(ChooseLevelAction);
            exitButton.onClick.AddListener(ExitAction);
        }

        private void OnDestroy()
        {
            continueButton.onClick.RemoveListener(ContinueAction);
            chooseLevelButton.onClick.RemoveListener(ChooseLevelAction);
            exitButton.onClick.RemoveListener(ExitAction);
        }
    
        private void ContinueAction()
        {
            OnContinueAction?.Invoke();
        }
        
        private void ChooseLevelAction()
        {
            OnChooseLevelAction?.Invoke();
        }
        
        private void ExitAction()
        {
            OnExitAction?.Invoke(); 
        }
    }
}