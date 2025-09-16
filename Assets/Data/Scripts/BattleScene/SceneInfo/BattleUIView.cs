using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Levels.Game
{
    public class BattleUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _waveLevelShower;
        [SerializeField] private TextMeshProUGUI _mainTowerHealth;
        [SerializeField] private TextMeshProUGUI _coins;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _startButton;

        public System.Action OnContinueButtonPress;
        public System.Action OnPauseButtonPress;
        public System.Action OnStartButtonPress;

        private void Awake()
        {
            _resumeButton.onClick.AddListener(() => { OnContinueButtonPress?.Invoke(); _resumeButton.gameObject.SetActive(false); _pauseButton.gameObject.SetActive(true); });
            _pauseButton.onClick.AddListener(() => { OnPauseButtonPress?.Invoke(); _resumeButton.gameObject.SetActive(true); _pauseButton.gameObject.SetActive(false); });
            _startButton.onClick.AddListener(() => OnStartButtonPress?.Invoke());

            _resumeButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveAllListeners();
            _startButton.onClick.RemoveAllListeners();
        }
        public void SetStartButtonState(bool isEnable) 
        { 
            _startButton.gameObject.SetActive(isEnable);
        }

        public void SetCoins(int value)
        {
            _coins.text = value.ToString();
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