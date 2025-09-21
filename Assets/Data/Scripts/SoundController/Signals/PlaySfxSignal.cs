using UnityEngine;

public class PlaySfxSignal
{
    public AudioClip Clip;
    public float Volume;

    public PlaySfxSignal(AudioClip clip, float volume = 1f)
    {
        Clip = clip;
        Volume = volume;
    }
}