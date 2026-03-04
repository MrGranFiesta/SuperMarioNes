using UnityEngine;

public class SoundtrackConst
{
    public string Value { get; }

    private SoundtrackConst(string value)
    {
        Value = value;
    }

    public AudioClip GetClip()
    {
        return ResourceManager.GetClip(this);
    }

    //Sountrack
    public static readonly SoundtrackConst MenuSoundtrack = new SoundtrackConst("MenuSoundtrack");
    public static readonly SoundtrackConst OverworldSoundtrack = new SoundtrackConst("OverworldSoundtrack");
    public static readonly SoundtrackConst UnderWorldSoundtrack = new SoundtrackConst("UnderWorldSoundtrack");
}
