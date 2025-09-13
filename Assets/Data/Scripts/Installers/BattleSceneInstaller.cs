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
    [Header("Battle views")]
    [SerializeField] private BattleFieldView _battleFieldView_1;
    [SerializeField] private BattleFieldView _battleFieldView_2;
    [SerializeField] private BattleFieldView _battleFieldView_3;

    [Header("Main Tower")]
    [SerializeField] private MainTowerView _mainTowerView;
    [SerializeField] private BattleUIView _battleUIView;
    
    [Header("Enemies")]
    [SerializeField] private EnemyView _enemyViewBase; 
    [SerializeField] private EnemyView _enemyViewWater;
    [SerializeField] private EnemyView _enemyViewFire; 
    
    [Header("Towers")]
    [SerializeField] private TowerView _towerViewBase; 
    [SerializeField] private TowerView _towerViewWater;
    [SerializeField] private TowerView _towerViewFire;

    [Header("Build Tower")]
    [SerializeField] private SpotBuildSubView _spotBuildingView;
    [SerializeField] private SpotBuildingSubUpgradeView _spotBuildingUpgradeView;

    public override void InstallBindings()
    {
        Container.BindFactory<EnemyModel, EnemyFabric>().AsSingle();
        Container.BindFactory<TowerModel, TowerFabric>().AsSingle();
        
        Container.Bind<BattleManager>().AsSingle().NonLazy();

        Container.BindFactory<BattleFieldView, BattleFieldView.BattleFieldViewFabric>().WithId("LEVEL_1").FromComponentInNewPrefab(_battleFieldView_1).AsCached();
        Container.BindFactory<BattleFieldView, BattleFieldView.BattleFieldViewFabric>().WithId("LEVEL_2").FromComponentInNewPrefab(_battleFieldView_2).AsCached();
        Container.BindFactory<BattleFieldView, BattleFieldView.BattleFieldViewFabric>().WithId("LEVEL_3").FromComponentInNewPrefab(_battleFieldView_3).AsCached();
        Container.Bind<BattleUIView>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind<MainTowerView>().FromComponentInNewPrefab(_mainTowerView).AsSingle();
        Container.Bind<MainTowerModel>().AsSingle().NonLazy();
        Container.Bind<MainTowerPresenter>().AsSingle().NonLazy();
        
        Container.Bind<BattleFieldPresenter>().AsSingle().NonLazy();
        Container.Bind<BattleUIPresenter>().AsSingle().NonLazy();

        Container.Bind<EnemyView>().WithId(EnemyType.Base.ToString()).FromComponentInNewPrefab(_enemyViewBase).AsTransient();
        Container.Bind<EnemyView>().WithId(EnemyType.Water.ToString()).FromComponentInNewPrefab(_enemyViewWater).AsTransient();
        Container.Bind<EnemyView>().WithId(EnemyType.Fire.ToString()).FromComponentInNewPrefab(_enemyViewFire).AsTransient();
    
        Container.Bind<TowerView>().WithId(BulletType.Base.ToString()).FromComponentInNewPrefab(_towerViewBase).AsTransient();
        Container.Bind<TowerView>().WithId(BulletType.Water.ToString()).FromComponentInNewPrefab(_towerViewWater).AsTransient();
        Container.Bind<TowerView>().WithId(BulletType.Fire.ToString()).FromComponentInNewPrefab(_towerViewFire).AsTransient();

        Container.Bind<BattleResultPresenter>().AsSingle().NonLazy();
        Container.Bind<BattleResultView>().FromComponentInHierarchy().AsSingle();

        Container.BindFactory<SpotBuildSubView, SpotBuildSubView.Factory>().FromComponentInNewPrefab(_spotBuildingView).AsSingle();
        Container.BindFactory<SpotBuildingSubUpgradeView, SpotBuildingSubUpgradeView.Factory>().FromComponentInNewPrefab(_spotBuildingUpgradeView).AsSingle();
    }
}
