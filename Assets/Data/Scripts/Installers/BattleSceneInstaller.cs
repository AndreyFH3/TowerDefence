using Levels.Enemies;
using Levels.Game;
using Levels.Info;
using Levels.Managers;
using Levels.Spawner;
using Levels.Tower;
using UnityEngine;
using Zenject;

public class BattleSceneInstaller : MonoInstaller
{
    [SerializeField] private BattleFieldView _battleFieldView_1; //вот по этой штуке вопросы есть.
    [SerializeField] private BattleFieldView _battleFieldView_2; //вот по этой штуке вопросы есть.
    [SerializeField] private BattleFieldView _battleFieldView_3; //вот по этой штуке вопросы есть.
    [SerializeField] private MainTowerView _mainTowerView; //вот по этой штуке вопросы есть.
    [SerializeField] private BattleUIView _battleUIView;
    
    [SerializeField] private EnemyView _enemyViewBase; //вот по этой штуке вопросы есть.
    [SerializeField] private EnemyView _enemyViewWater; //вот по этой штуке вопросы есть.
    [SerializeField] private EnemyView _enemyViewFire; //вот по этой штуке вопросы есть.

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
        
        Container.Bind<MainTowerModel>().AsSingle().NonLazy();
        Container.Bind<BattleFieldPresenter>().AsSingle().NonLazy();
        Container.Bind<BattleUIPresenter>().AsSingle().NonLazy();
        Container.Bind<MainTowerPresenter>().AsSingle().NonLazy();

        Container.Bind<EnemyView>().WithId(EnemyType.Base.ToString()).FromComponentInNewPrefab(_enemyViewBase).AsCached();
        Container.Bind<EnemyView>().WithId(EnemyType.Water.ToString()).FromComponentInNewPrefab(_enemyViewWater).AsCached();
        Container.Bind<EnemyView>().WithId(EnemyType.Fire.ToString()).FromComponentInNewPrefab(_enemyViewFire).AsCached();
    
        Container.Bind<TowerView>().WithId(BulletType.Base.ToString()).AsCached();
        Container.Bind<TowerView>().WithId(BulletType.Water.ToString()).AsCached();
        Container.Bind<TowerView>().WithId(BulletType.Fire.ToString()).AsCached();

    }
}
