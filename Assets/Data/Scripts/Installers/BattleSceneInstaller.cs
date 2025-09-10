using Levels.Enemies;
using Levels.Game;
using Levels.Info;
using Levels.Managers;
using Levels.Spawner;
using Levels.Tower;
using UnityEngine;
using Zenject;
using static Levels.Game.BattleFieldView;

public class BattleSceneInstaller : MonoInstaller
{
    [SerializeField] private BattleFieldView _battleFieldView_1; //вот по этой штуке вопросы есть.
    [SerializeField] private BattleFieldView _battleFieldView_2; //вот по этой штуке вопросы есть.
    [SerializeField] private BattleFieldView _battleFieldView_3; //вот по этой штуке вопросы есть.
    [SerializeField] private MainTowerView _mainTowerView; //вот по этой штуке вопросы есть.
    [SerializeField] private BattleUIView _battleUIView;
    public override void InstallBindings()
    {
        Container.BindFactory<EnemyModel, EnemyFabric>().AsSingle();
        Container.BindFactory<TowerModel, TowerFabric>().AsSingle();
        
        Container.Bind<BattleManager>().AsSingle().NonLazy();

        Container.BindFactory<BattleFieldView, BattleFieldView.BattleFieldViewFabric>().WithId("LEVEL_1").FromComponentInNewPrefab(_battleFieldView_1).AsCached();
        Container.BindFactory<BattleFieldView, BattleFieldView.BattleFieldViewFabric>().WithId("LEVEL_2").FromComponentInNewPrefab(_battleFieldView_2).AsCached();
        Container.BindFactory<BattleFieldView, BattleFieldView.BattleFieldViewFabric>().WithId("LEVEL_3").FromComponentInNewPrefab(_battleFieldView_3).AsCached();
        Container.Bind<MainTowerView>().FromComponentInNewPrefab(_mainTowerView).AsSingle();
        Container.Bind<BattleUIView>().FromComponentInNewPrefab(_battleUIView).AsSingle();
        
        Container.Bind<BattleFieldPresenter>().AsSingle().NonLazy();
        Container.Bind<BattleUIPresenter>().AsSingle().NonLazy();
        Container.Bind<MainTowerPresenter>().AsSingle().NonLazy();

        Container.Bind<EnemyView>().WithId(EnemyType.Base.ToString()).AsCached();
        Container.Bind<EnemyView>().WithId(EnemyType.Water.ToString()).AsCached();
        Container.Bind<EnemyView>().WithId(EnemyType.Fire.ToString()).AsCached();
    
        Container.Bind<TowerView>().WithId(BulletType.Base.ToString()).AsCached();
        Container.Bind<TowerView>().WithId(BulletType.Water.ToString()).AsCached();
        Container.Bind<TowerView>().WithId(BulletType.Fire.ToString()).AsCached();

    }
}
