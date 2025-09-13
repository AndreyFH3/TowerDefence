using UnityEngine;
using UnityEngine.UI;

namespace Menu.LevelSelect
{
    public class LevelSelectionView : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private Button backgroundButton;
        [SerializeField] private Button closeButton;

        public System.Action OnClose;

        private void Awake()
        {
            backgroundButton.onClick.AddListener(Close);
            closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            backgroundButton.onClick.RemoveListener(Close);
            closeButton.onClick.RemoveListener(Close);
        }

        public void SetParent(Transform transform)
        {
            transform.SetParent(parent);
            transform.localScale = Vector3.one;
        }

        public void Close()
        {
            Destroy(gameObject);
            OnClose?.Invoke();
        }

        public class Factory : Zenject.PlaceholderFactory<LevelSelectionView> { }
    }
}