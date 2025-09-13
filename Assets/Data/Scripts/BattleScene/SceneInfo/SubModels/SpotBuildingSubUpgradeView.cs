using UnityEngine;
using UnityEngine.UI;

public class SpotBuildingSubUpgradeView : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _closeButton;

    public System.Action OnUpgradeButtonClick;
    public System.Action OnSellButtonClick;

    private void Awake()
    {
        _upgradeButton.onClick.AddListener(() => OnUpgradeButtonClick?.Invoke());
        _sellButton.onClick.AddListener(() => OnSellButtonClick?.Invoke());
        _closeButton.onClick.AddListener(Close);
    }

    private void Close() => Destroy(gameObject);
    private void OnDestroy()
    {
        _upgradeButton.onClick.RemoveAllListeners();
        _sellButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    public class Factory : Zenject.PlaceholderFactory<SpotBuildingSubUpgradeView> { }
}
