using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI percentsText;
        [SerializeField] private Slider sliderValue;

        //private void Awake()
        //{
        //    transform.SetParent(null);
        //    DontDestroyOnLoad(gameObject);
        //}

        public void SetLoadingValue(float value)
        {
            percentsText.text = $"{Mathf.Round(value * 100)}%";
            sliderValue.value = value;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            SetLoadingValue(0);
        }
    }
}