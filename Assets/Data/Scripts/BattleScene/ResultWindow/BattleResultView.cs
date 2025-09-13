using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Levels.Game
{
    public class BattleResultView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private Button _home;
        [SerializeField] private Button _next;

        public System.Action OnHomeButtonClick;
        public System.Action OnNextButtonClick;

        private void Awake()
        {
            _home.onClick.AddListener(()=> OnHomeButtonClick?.Invoke());
            _next.onClick.AddListener(()=> OnNextButtonClick?.Invoke());
        }

        public void SetResults(bool isWin)
        {
            _resultText.text = isWin ? "Победа" : "Поражение";
        }
    }
}