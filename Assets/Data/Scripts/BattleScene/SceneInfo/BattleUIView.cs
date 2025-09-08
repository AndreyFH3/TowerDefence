using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace Levels.Game
{
    public class BattleUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _waveLevelShower;
        [SerializeField] private TextMeshProUGUI _mainTowerHealth;
        [SerializeField] private Button _pauseButton;

        public System.Action OnPauseButtonPress;

        private void Awake()
        {
            _pauseButton.onClick.AddListener(() => OnPauseButtonPress?.Invoke());
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveAllListeners();
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