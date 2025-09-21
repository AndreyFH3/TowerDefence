using UnityEngine;

public class PlayMusicSignal
{
    public AudioClip Clip;
    public float Volume;
    public bool Loop;

    public PlayMusicSignal(AudioClip clip, float volume = 1f, bool loop = true)
    {
        Clip = clip;
        Volume = volume;
        Loop = loop;
    }
}