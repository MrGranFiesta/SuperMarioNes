using UnityEngine;

public class MainClass
{
    public static PlayerManager Player;
    public static I18n I18n;
    public static CustomEvents CustomEvents;
    public static Config Config;
    public static AudioManager AudioManager;
    public static FireManager FireManager;
    public static Datastore Datastore;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Start()
    {
        Player = new PlayerManager();
        CustomEvents = new CustomEvents();
        I18n = new I18n();
        Datastore = new Datastore();
        Config = new Config();
        AudioManager = new AudioManager();
        FireManager = new FireManager();
    }
}
