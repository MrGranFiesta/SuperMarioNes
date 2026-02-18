using UnityEngine;

public class AudioManager
{
    private GameObject _go;
    public AudioPooling AudioPooling;

    public AudioManager()
    {
        _go = new GameObject("AudioManager");
        Object.DontDestroyOnLoad(_go);
        AudioPooling = new AudioPooling(_go);
    }
}