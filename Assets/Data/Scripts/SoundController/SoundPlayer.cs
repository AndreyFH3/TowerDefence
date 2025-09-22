using Zenject;

namespace Sounds
{
    public class SoundPlayer 
    {
        private SignalBus _signalBus;
        private SoundContainer _soundContainer;

        [Inject]
        public void Init(SignalBus signalBus, SoundContainer soundContainer)
        {
            _signalBus = signalBus;
            _soundContainer = soundContainer;
        }

        public void PlayClickSound()
        {
            var clip = _soundContainer.GetSfx("CLICK");
            _signalBus.Fire<PlaySfxSignal>(new(clip));
        }

        public void PlayEnemyDieSound()
        {
            var clip = _soundContainer.GetSfx("ENEMY_DIE");
            _signalBus.Fire<PlaySfxSignal>(new(clip));
        }

        public void PlayWinSound()
        {
            var clip = _soundContainer.GetSfx("GAME_WIN");
            _signalBus.Fire<PlaySfxSignal>(new(clip));
        }

        public void PlayLoseSound()
        {
            var clip = _soundContainer.GetSfx("GAME_LOSE");
            _signalBus.Fire<PlaySfxSignal>(new(clip));
        }

        public void PlayTowerShootSound()
        {
            var clip = _soundContainer.GetSfx("TOWER_SHOOT");
            _signalBus.Fire<PlaySfxSignal>(new(clip));
        }

        public void PlayBattleMusic()
        {
            var clip = _soundContainer.GetMusic("BATTLE_MUSIC");
            _signalBus.Fire<PlayMusicSignal>(new(clip));
        }

        public void PlayMainSceneMusic()
        {
            var clip = _soundContainer.GetMusic("MAIN_SCENE");
            _signalBus.Fire<PlayMusicSignal>(new(clip));
        }

    }
}
