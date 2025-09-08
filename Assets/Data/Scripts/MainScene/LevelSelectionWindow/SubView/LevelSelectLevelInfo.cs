using Levels.Info;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace Menu.LevelSelect
{
    public class LevelSelectLevelInfo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelName;
        [SerializeField] private Image levelIcon;
        [SerializeField] private Button levelButton;

        public System.Action OnClick;

        private void Awake()
        {
            levelButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            levelButton.onClick.RemoveListener(OnButtonClick);
        }

        public void SetData(LevelInfo info)
        {
            levelName.text = info.LevelId;
            levelIcon.sprite = info.Sprite;
        }

        private void OnButtonClick()
        {
            OnClick?.Invoke();
        }


        public class Factory : Zenject.PlaceholderFactory<LevelSelectLevelInfo>
        {
        }
    }
}