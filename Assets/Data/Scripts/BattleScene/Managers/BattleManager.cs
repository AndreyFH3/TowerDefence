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
using Zenject;

namespace Levels.Managers
{
    public class BattleManager
    {
        private List<EnemyModel> _enemies = new();
        private List<TowerModel> _towers = new();
        private List<Vector3> _points;
        private List<Wave> _waves;

        private EnemyFabric _enemyFabric;
        private TowerFabric _towerFabric;
        private MainTowerModel _mainTower;

        private int _currentWave = 0;

        public System.Action OnBattleStart;
        public System.Action OnBattleWin;
        public System.Action OnBattleLose;
        public System.Action OnTowerBuild;
        public System.Action OnWaveStarted;
        public System.Action OnWaveFinished;
        public System.Action<float> OnMainTowerDamaged;

        public int Health => Mathf.RoundToInt(_mainTower.Health);
        
        public Vector3[] Points => _points.ToArray();
        public Wave[] Waves => _waves.ToArray();

        public System.Action<EnemyModel> OnEnemyDie;

        private float UnitSpawnDelay => 1.5f;

        private CancellationTokenSource _cts;

        [Inject]
        public void Init(EnemyFabric enemyFabric, TowerFabric towerFabric, MainTowerModel mainTower)
        {
            _enemyFabric = enemyFabric;
            _towerFabric = towerFabric;
            _mainTower = mainTower;

            _mainTower.OnDamaged += MainTowerDamaged;
            _mainTower.OnDie += Lose;

            _cts = new();
        }

        public void SetWaves(List<Wave> waves)
        {
            _waves = waves;
        }

        public void SetPoints(List<Vector3> points)
        {
            _points = points;
        }

        public (int, int) GetWavesInfo() => (_currentWave + 1, Waves.Length);

        private void MainTowerDamaged(float value) => OnMainTowerDamaged?.Invoke(value);

        private void Win()
        {
            if(_currentWave >= Waves.Length)
                OnBattleWin?.Invoke();
        }
        private void Lose() 
        {
            OnBattleLose?.Invoke();
        }

        public void StartWave()
        {
            if (_enemies.Count > 0 && _currentWave <= Waves.Length)
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


        public TowerModel AddTower(Vector3 position, BulletType type)
        {
            TowerModel tower = _towerFabric.Create(position, type, this);
            _towers.Add(tower);
            return tower;
        }

        private async UniTask CreateWave(CancellationToken token)
        {
            try
            {
                OnWaveStarted?.Invoke();
                foreach (var enemyData in Waves[_currentWave].Enemies)
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
            if(_currentWave >= Waves.Length)
                OnBattleWin?.Invoke();
            Win();
        }

        private void SpawnEnemy(string id)
        {
            var enemy = _enemyFabric.Create(Points[0], id, this);
            
            enemy.OnDie += RemoveEnemy;
            enemy.OnDamageMainTower += RemoveEnemy;
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
            enemy.OnDamageMainTower -= RemoveEnemy;
            enemy.OnDamageMainTower -= DamageMainTower;

            _enemies.Remove(enemy);
            CheckWaveFinish();
        }
    }
}
