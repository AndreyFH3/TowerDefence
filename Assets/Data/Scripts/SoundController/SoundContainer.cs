using UnityEngine;
[CreateAssetMenu(fileName = "SoundData", menuName = "Game/SoundDatabase")]
public class SoundContainer : ScriptableObject
{
    [System.Serializable]
    public class SoundEntry
    {
        public string Id;
        public AudioClip Clip;
    }

    public SoundEntry[] _sfxSounds;
    public SoundEntry[] _music;

    public AudioClip GetSfx(string id)
    {
        foreach (var s in _sfxSounds)
            if (s.Id == id)
                return s.Clip;
        return null;
    }

    public AudioClip GetMusic(string id)
    {
        foreach (var s in _music)
            if (s.Id == id)
                return s.Clip;
        return null;
    }
}
