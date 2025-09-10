using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Levels.Game
{
    public class BattleUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _waveLevelShower;
        [SerializeField] private TextMeshProUGUI _mainTowerHealth;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _startButton;

        public System.Action OnPauseButtonPress;
        public System.Action OnStartButtonPress;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(() => OnPauseButtonPress?.Invoke());
            _startButton.onClick.AddListener(() => OnStartButtonPress?.Invoke());
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
        }

        public void SetWaveInfo(string text)
        {
            _waveLevelShower.text = text;
        }

        public void SetHealthInfo(string text)
        {
            _mainTowerHealth.text = text;
        }
    }
}