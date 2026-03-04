using UnityEngine;

public class SoundtrackManager
{
    private GameObject _go;
    public SoundtrackConst soundtrack;
    private AudioSource _audioSource;

    public SoundtrackManager(GameObject parent)
    {
        _go = parent;
        _go.AddComponent<AudioSource>();
        _audioSource = _go.GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
