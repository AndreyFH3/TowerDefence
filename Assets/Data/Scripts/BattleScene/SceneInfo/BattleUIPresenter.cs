using System.Linq;
using Levels.Info;
using Levels.Managers;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Levels.Game
{
    public class BattleUIPresenter
    {
        private BattleUIView _view;
        private BattleManager _model;
        private LevelInfo _levelInfo;
        private Wallet _wallet;

        [Inject]
        public void Init(BattleUIView view, BattleManager model, LevelInfoContainer container, LevelSceneInfo sceneInfo)
        {
            _view = view;
            _model = model;
            _wallet = _model.Wallet;
            _levelInfo = container.GetLevelInfo(sceneInfo.LevelId);

            _model.SetPoints(_levelInfo.Points);
            _model.SetWaves(_levelInfo.Waves);

            _model.OnMainTowerDamaged += SetHealth;

            _model.OnWaveStarted += SetWaves;
            _model.OnWaveFinished += ActiveStartButtonState;
            
            _view.OnStartButtonPress += StartWave;
            _view.OnPauseButtonPress += _model.Pause;
            _view.OnContinueButtonPress += _model.Resume;
            _model.Wallet.OnCoinsValueChanged += SetWalletValue;

            SetHealth(_model.Health);
            SetWaves();
            SetWalletValue();
        }
        

        private void SetWalletValue()
        {
            _view.SetCoins(_wallet.Coins);
        }

        private void StartWave()
        {
            _model.StartWave();
            DisableStartButtonState();
        }

        private void ActiveStartButtonState() 
        {
            _view.SetStartButtonState(true);
        }
        private void DisableStartButtonState() 
        {
            _view.SetStartButtonState(false);
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