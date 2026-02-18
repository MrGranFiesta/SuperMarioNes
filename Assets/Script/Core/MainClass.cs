using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MainClass
{
    public static PlayerManager Player;
    public static I18n I18n;
    public static CustomEvents CustomEvents;
    public static Config Config;
    public static AudioManager AudioManager;
    public static FireManager FireManager;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Start()
    {
        Player = new PlayerManager();
        I18n = new I18n();
        CustomEvents = new CustomEvents();
        Config = new Config();
        AudioManager = new AudioManager();
        FireManager = new FireManager();
    }
}
