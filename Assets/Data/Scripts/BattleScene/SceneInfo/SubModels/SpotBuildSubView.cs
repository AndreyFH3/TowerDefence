using UnityEngine;
using UnityEngine.UI;

public class SpotBuildSubView : MonoBehaviour
{
    [SerializeField] private Button _tryBuyBase;
    [SerializeField] private Button _tryBuyFire;
    [SerializeField] private Button _tryBuyWater;

    [SerializeField] private Button _close;

    public System.Action OnTryBuyBase;
    public System.Action OnTryBuyFire;
    public System.Action OnTryBuyWater;

    private void Awake()
    {
        _tryBuyBase.onClick.AddListener(() => OnTryBuyBase?.Invoke());
        _tryBuyFire.onClick.AddListener(() => OnTryBuyFire?.Invoke());
        _tryBuyWater.onClick.AddListener(() => OnTryBuyWater?.Invoke());
        _close.onClick.AddListener(Close);
    }

    private void Close() => Destroy(this.gameObject);

    private void OnDestroy()
    {
        _tryBuyBase.onClick.RemoveAllListeners();
        _tryBuyFire.onClick.RemoveAllListeners();
        _tryBuyWater.onClick.RemoveAllListeners();
        _close.onClick.RemoveAllListeners();
    }

    public class Factory : Zenject.PlaceholderFactory<SpotBuildSubView> { }
}
