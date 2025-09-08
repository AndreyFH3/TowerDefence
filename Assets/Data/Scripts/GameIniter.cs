using Cysharp.Threading.Tasks;
using Game.Core;
using Zenject;

public class GameIniter
{
    [Inject]
    public async UniTask InitGame(LoadingScreenPresenter presenter)
    {
        //тут подгрузка всяких конфигов, авторизация и прочие вещи, которые нужно сделать, чтобы игра работала как надо ну и в конце - переход на нужную сцену, код которой ниже.
        presenter.LoadBootstrap();
        await UniTask.WaitForSeconds(3);
        presenter.LoadBaseScene();
    }
}
