using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Mono.Cecil.Cil;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxPrefab;
    [SerializeField] private int _poolSize = 5;

    private readonly Queue<AudioSource> _pool = new();

    public void Init()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            var src = Instantiate(_sfxPrefab, transform);
            src.playOnAwake = false;
            src.gameObject.SetActive(true);
            _pool.Enqueue(src);
        }
    }

    public void PlayMusic(AudioClip clip, float volume, bool loop)
    {
        _musicSource.clip = clip;
        _musicSource.volume = volume;
        _musicSource.loop = loop;
        _musicSource.Play();
    }

    public void StopMusic() => _musicSource.Stop();

    public void PlaySfx(AudioClip clip, float volume)
    {
        if (_pool.Count == 0) return;

        var src = _pool.Dequeue();
        src.gameObject.SetActive(true);
        src.clip = clip;
        src.volume = volume;
        src.loop = false;
        src.Play();
        Release(src).Forget();
    }

    private async UniTask Release(AudioSource src)
    {
        try
        {
            await UniTask.WaitForSeconds(src.clip.length);
            src.Stop();
            src.clip = null;
            src.gameObject.SetActive(false);
            _pool.Enqueue(src);
        }
        catch
        {
            if (src == null)
                return;

            src.Stop();
            src.clip = null;
            src.gameObject.SetActive(false);
            _pool.Enqueue(src);
        }
    }
}
