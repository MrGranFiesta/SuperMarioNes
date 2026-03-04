using UnityEngine;

public class AudioManager
{
    private GameObject _audioPoolingGO;
    private GameObject _soundtrackGO;
    public AudioPooling AudioPooling;
    public SoundtrackManager SoundtrackManager;

    public AudioManager()
    {
        //AudioPooling
        _audioPoolingGO = new GameObject("AudioPooling");
        Object.DontDestroyOnLoad(_audioPoolingGO);
        AudioPooling = new AudioPooling(_audioPoolingGO);

        //SoundtrackManager
        _soundtrackGO = new GameObject("SoundtrackGO");
        Object.DontDestroyOnLoad(_soundtrackGO);
        SoundtrackManager = new SoundtrackManager(_soundtrackGO);
        SoundtrackManager.Play(SoundtrackConst.MenuSoundtrack.GetClip());
    }
}