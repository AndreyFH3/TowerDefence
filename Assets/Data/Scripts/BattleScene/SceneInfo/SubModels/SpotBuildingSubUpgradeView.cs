using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpotBuildingSubUpgradeView : MonoBehaviour
{
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TextMeshProUGUI _price;

    public System.Action OnUpgradeButtonClick;
    public System.Action OnSellButtonClick;
    public System.Action OnClose;

    private void Awake()
    {
        _upgradeButton.onClick.AddListener(() => { OnUpgradeButtonClick?.Invoke(); Close(); });
        _sellButton.onClick.AddListener(() => { OnSellButtonClick?.Invoke(); Close(); });
        _closeButton.onClick.AddListener(Close);
    }
    
    private void Close()
    {
        Destroy(gameObject);
        OnClose?.Invoke();
    }

    public void SetUpgradeButtonInteractable( bool value)
    {
        _upgradeButton.interactable = value;
    }

    public void DisableUpgradeButton(bool value)
    {
        _upgradeButton.gameObject.SetActive(value); 
    }

    public void SetPrice(int value) => _price.text = value.ToString();
        
    private void OnDestroy()
    {
        _upgradeButton.onClick.RemoveAllListeners();
        _sellButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    public class Factory : Zenject.PlaceholderFactory<SpotBuildingSubUpgradeView> { }
}
