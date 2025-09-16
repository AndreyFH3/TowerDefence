using Levels;
using Levels.Info;
using Zenject;

public class Wallet
{
    private int _coins;

    public System.Action OnCoinsValueChanged;

    public int Coins => _coins;

    [Inject]
    public void Init(LevelInfoContainer _container, LevelSceneInfo info)
    {
        _coins = _container.GetLevelInfo(info.LevelId)?.CoinsDefault ?? 0;
    }

    public bool TrySpend(int value)
    {
        if (_coins - value < 0)
            return false;

        _coins -= value;
        OnCoinsValueChanged?.Invoke();
        return true;
    }

    public bool CheckEnough(int value)
    {
        if (_coins - value < 0)
            return false;

        return true;
    }

    public void AddCoins(int value)
    {
        _coins += value;
        OnCoinsValueChanged?.Invoke();
    }
}
