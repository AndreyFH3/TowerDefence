using UnityEngine;
using UnityEngine.UI;

namespace Levels.Tower
{
    public class BuildSpotView : MonoBehaviour
    {
        [SerializeField] private Button based;
        [SerializeField] private Button water;
        [SerializeField] private Button fire;
        [SerializeField] private Button destroy;

        public System.Action OnBasedPressed;
        public System.Action OnWaterPressed;
        public System.Action OnFirePressed;

        private void Awake()
        {
            based.onClick.AddListener(() => OnBasedPressed?.Invoke());
            water.onClick.AddListener(() => OnWaterPressed?.Invoke());
            fire.onClick.AddListener(() => OnFirePressed?.Invoke());
            destroy.onClick.AddListener(Close);
        }

        private void Close() => Destroy(gameObject);

        private void OnDestroy()
        {
            based.onClick.RemoveAllListeners();
            water.onClick.RemoveAllListeners();
            fire.onClick.RemoveAllListeners();
            destroy.onClick.RemoveAllListeners();
        }
    }

}