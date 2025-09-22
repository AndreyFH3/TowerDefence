using UnityEngine;
using Zenject;

public class SoundPresenter
{
    private readonly SoundModel _model;
    private readonly SoundController _view;
    private readonly SignalBus _signalBus;

    public SoundPresenter(SoundModel model, SoundController view, SignalBus signalBus)
    {
        _model = model;
        _view = view;
        _signalBus = signalBus;

        Init();
    }

    public void Init()
    {
        _view.Init();
        _signalBus.Subscribe<PlaySfxSignal>(Handle);
        _signalBus.Subscribe<PlayMusicSignal>(Handle);
    }


    public void Handle(PlaySfxSignal signal)
    {
        _view.PlaySfx(signal.Clip, signal.Volume * _model.SfxVolume);
    }

    public void Handle(PlayMusicSignal signal)
    {
        _view.PlayMusic(signal.Clip, signal.Volume * _model.MusicVolume, signal.Loop);
    }
}
