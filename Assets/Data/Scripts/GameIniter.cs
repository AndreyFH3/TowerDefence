using Cysharp.Threading.Tasks;
using Game.Core;
using Zenject;

public class GameIniter
{
    [Inject]
    public async UniTask InitGame(LoadingScreenPresenter presenter)
    {
        //��� ��������� ������ ��������, ����������� � ������ ����, ������� ����� �������, ����� ���� �������� ��� ���� �� � � ����� - ������� �� ������ �����, ��� ������� ����.
        presenter.LoadBootstrap();
        await UniTask.WaitForSeconds(3);
        presenter.LoadBaseScene();
    }
}
