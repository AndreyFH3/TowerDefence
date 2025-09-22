using System.Linq;
using System.Runtime.CompilerServices;
using Levels.Info;
using Levels.Managers;
using Sounds;
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
        private SoundPlayer _soundPlayer;

        [Inject]
        public void Init(BattleUIView view, BattleManager model, LevelInfoContainer container, LevelSceneInfo sceneInfo, SoundPlayer soundPlayer)
        {
            _view = view;
            _model = model;
            _wallet = _model.Wallet;
            _soundPlayer = soundPlayer;
            _levelInfo = container.GetLevelInfo(sceneInfo.LevelId);

            _model.SetPoints(_levelInfo.Points);
            _model.SetWaves(_levelInfo.Waves);

            _model.OnMainTowerDamaged += SetHealth;

            _model.OnWaveStarted += SetWaves;
            _model.OnWaveFinished += ActiveStartButtonState;
            
            _view.OnStartButtonPress += StartWave;
            _view.OnPauseButtonPress += SetPause;
            _model.Wallet.OnCoinsValueChanged += SetWalletValue;

            SetHealth(_model.Health);
            SetWaves();
            SetWalletValue();
        }
        
        private void SetPause()
        {
            _model.Pause();
            _soundPlayer.PlayClickSound();
        }

        private void SetWalletValue()
        {
            _view.SetCoins(_wallet.Coins);
        }

        private void StartWave()
        {
            _model.StartWave();
            _soundPlayer.PlayClickSound();
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