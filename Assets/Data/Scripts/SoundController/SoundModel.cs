using UnityEngine;

public class SoundModel
{
    public float MusicVolume { get; private set; } = 1f;
    public float SfxVolume { get; private set; } = 1f;

    public void SetMusicVolume(float value) => MusicVolume = value;
    public void SetSfxVolume(float value) => SfxVolume = value;
}