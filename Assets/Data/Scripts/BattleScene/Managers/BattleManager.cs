using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Levels.Info;
using Levels.Enemies;
using Levels.Spawner;
using Levels.Tower;
using Levels.Info.Tower;

namespace Levels.Managers
{
    public class BattleManager
    {
        private List<EnemyModel> _enemies = new();
        private List<TowerModel> _towers = new();
        private List<MapCell> _cells = new();

        private EnemyFabric _enemyFabric;
        private TowerFabric _towerFabric;
        private MainTowerModel _mainTower;

        private int _currentWave = 0;

        public System.Action OnBattleStart;
        public System.Action OnBattleWin;
        public System.Action OnBattleLose;
        public System.Action OnTowerBuild;
        public System.Action OnWaveFinished;
        public System.Action<float> OnMainTowerDamaged;
        

        public System.Action<EnemyModel> OnEnemyDie;

        public MapCell[] Cells => _cells.ToArray();
        private LevelInfo _levelInfo;

        private float UnitSpawnDelay => 1.5f;

        private CancellationTokenSource _cts;

        public void Init(LevelInfoContainer levelsContainer, LevelSceneInfo levelSceneInfo, EnemyFabric enemyFabric, TowerFabric towerFabric, MainTowerModel mainTower)
        {
            _levelInfo = levelsContainer.GetLevelInfo(levelSceneInfo.LevelId);
            _enemyFabric = enemyFabric;
            _towerFabric = towerFabric;
            _mainTower = mainTower;

            _mainTower.OnDamaged += MainTowerDamaged;

            _cts = new();
        }

        public (int, int) GetWavesInfo() => (_currentWave + 1, _levelInfo.Waves.Length);

        private void MainTowerDamaged(float value) => OnMainTowerDamaged?.Invoke(value);

        private void Win() => OnBattleWin?.Invoke();
        private void Lose() => OnBattleLose?.Invoke();

        public void StartWave()
        {
            if (_enemies.Count > 0 && _currentWave >= _levelInfo.Waves.Length)
                return;
            CreateWave(_cts.Token).Forget();
        }

        public EnemyModel GetClosestUnit(Vector3 position)
        {
            EnemyModel enemy = _enemies
                        .OrderBy(e => Vector3.Distance(e.CurrentPosition, position))
                        .FirstOrDefault();
            return enemy;
        }

        private void AddTower(Vector3 position, TowerData data, BulletType bulletType)
        {
            TowerModel tower = _towerFabric.Create(position, data, this);
            _towers.Add(tower);    
        }

        private async UniTask CreateWave(CancellationToken token)
        {
            try
            {
                foreach (var enemyData in _levelInfo.Waves[_currentWave])
                {
                    SpawnEnemy(enemyData);
                    await UniTask.WaitForSeconds(UnitSpawnDelay, true, PlayerLoopTiming.Update, token);
                } 
            }
            catch (System.OperationCanceledException)
            {
                Debug.Log("Wave Creation Canceled!");
            }
        }

        private void CheckWaveFinish()
        {
            if (_enemies.Count > 0)
                return;

            _currentWave++;
            OnWaveFinished?.Invoke();
            if(_currentWave >= _levelInfo.Waves.Length)
                OnBattleWin?.Invoke();
        }

        private void SpawnEnemy(EnemyData data)
        {
            var enemy = _enemyFabric.Create(Cells[0].WorldPosition, data, this);
            
            enemy.OnDie += RemoveEnemy;
            enemy.OnDamageMainTower += DamageMainTower;

            _enemies.Add(enemy);
        }

        public void DamageMainTower(EnemyModel enemy)
        {
            _mainTower.GetDamage(enemy.Damage);
        }

        private void RemoveEnemy(EnemyModel enemy)
        {
            enemy.OnDie -= RemoveEnemy;
            enemy.OnDamageMainTower -= DamageMainTower;

            _enemies.Remove(enemy);
            CheckWaveFinish();
        }
    }
}
