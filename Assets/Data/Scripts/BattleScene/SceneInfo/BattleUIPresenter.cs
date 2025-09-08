using Levels.Managers;
using UnityEngine;
using Zenject;

namespace Levels.Game
{
    public class BattleUIPresenter
    {
        private BattleUIView _view;
        private BattleManager _model;

        [Inject]
        public void Init(BattleUIView view, BattleManager model)
        {
            _view = view;
            _model = model;

            _model.OnMainTowerDamaged += SetHealth;
            _model.OnWaveFinished += SetWaves;
        }

        public void SetHealth(float value)
        {
            _view.SetHealthInfo($"{Mathf.RoundToInt(value)}");
        }

        public void SetWaves()
        {
            var data = _model.GetWavesInfo();
            _view.SetWaveInfo($"{data.Item1}/{data.Item2}");
        }
    }
}