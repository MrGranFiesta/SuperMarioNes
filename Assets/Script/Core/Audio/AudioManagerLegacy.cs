using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerLegacy : MonoBehaviour
{
    private static AudioManagerLegacy _instance;

    public static AudioManagerLegacy Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManagerLegacy>();
                if (_instance == null)
                {
                    GameObject gm = new GameObject("GameManager");
                    _instance = gm.AddComponent<AudioManagerLegacy>();

                    // Inicializamos el AudioManager
                    _instance.AudioPooling = new AudioPooling(gm);
                }
            }
            return _instance;
        }
    }

    public AudioPooling AudioPooling;
}
