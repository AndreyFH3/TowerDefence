using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpotBuildSubView : MonoBehaviour
{
    [SerializeField] private Button _tryBuyBase;
    [SerializeField] private Button _tryBuyFire;
    [SerializeField] private Button _tryBuyWater;
    [SerializeField] private TextMeshProUGUI _tryBuyBaseText;
    [SerializeField] private TextMeshProUGUI _tryBuyFireText;
    [SerializeField] private TextMeshProUGUI _tryBuyWaterText;

    [SerializeField] private Button _close;

    public System.Action OnTryBuyBase;
    public System.Action OnTryBuyFire;
    public System.Action OnTryBuyWater;
    public System.Action OnClose;

    private void Awake()
    {
        _tryBuyBase.onClick.AddListener(() => { OnTryBuyBase?.Invoke(); Close(); });
        _tryBuyFire.onClick.AddListener(() => { OnTryBuyFire?.Invoke(); Close(); });
        _tryBuyWater.onClick.AddListener(() => { OnTryBuyWater?.Invoke(); Close(); });
        _close.onClick.AddListener(Close);
    }

    public void SetBasePrice(int value)
    {
        _tryBuyBaseText.text = value.ToString();
    }
    public void SetFirePrice(int value)
    {
        _tryBuyFireText.text = value.ToString();
    }
    public void SetWaterPrice(int value)
    {
        _tryBuyWaterText.text = value.ToString();
    }

    private void Close()
    {
        OnClose?.Invoke();
        Destroy(this.gameObject);
    }
    public void SetBaseButtonInteraction(bool value)
    {
        _tryBuyBase.interactable = value;
    }

    public void SetFireButtonInteraction(bool value)
    {
        _tryBuyFire.interactable = value;
    }
    public void SetWaterButtonInteraction(bool value)
    {
        _tryBuyWater.interactable = value;
    }

    private void OnDestroy()
    {
        _tryBuyBase.onClick.RemoveAllListeners();
        _tryBuyFire.onClick.RemoveAllListeners();
        _tryBuyWater.onClick.RemoveAllListeners();
        _close.onClick.RemoveAllListeners();
    }

    public class Factory : Zenject.PlaceholderFactory<SpotBuildSubView> { }
}
