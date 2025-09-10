using System.Linq;
using Levels.Info;
using Levels.Managers;
using UnityEngine;
using Zenject;

namespace Levels.Game
{
    public class BattleUIPresenter
    {
        private BattleUIView _view;
        private BattleManager _model;
        private LevelInfo _levelInfo;

        [Inject]
        public void Init(BattleUIView view, BattleManager model, LevelInfoContainer container, LevelSceneInfo sceneInfo)
        {
            _view = view;
            _model = model;

            _levelInfo = container.GetLevelInfo(sceneInfo.LevelId);

            _model.SetPoints(_levelInfo.Points);
            _model.SetWaves(_levelInfo.Waves);

            _model.OnMainTowerDamaged += SetHealth;
            _model.OnWaveFinished += SetWaves;
            _view.OnStartButtonPress += StartWave;
        }

        private void StartWave()
        {
            _model.StartWave();
        }

        private void SetHealth(float value)
        {
            _view.SetHealthInfo($"{Mathf.RoundToInt(value)}");
        }

        private void SetWaves()
        {
            var data = _model.GetWavesInfo();
            _view.SetWaveInfo($"{data.Item1}/{data.Item2}");
        }
    }
}