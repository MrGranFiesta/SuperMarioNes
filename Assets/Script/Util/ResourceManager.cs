using UnityEngine;

public class ResourceManager
{
    public static GameObject GetPrefabItem(PowerUp content)
    {
        return Resources.Load<GameObject>($"Prefab/Items/{content}");
    }

    public static GameObject GetCoinAnimation()
    {
        return Resources.Load<GameObject>($"Prefab/Items/CoinAnimation");
    }

    public static GameObject GetFired()
    {
        return Resources.Load<GameObject>($"Prefab/Items/Fired");
    }

    public static AudioClip GetClip(SoundConst audio)
    {
        return GetClip(audio.Value);
    }

    public static AudioClip GetClip(SoundtrackConst audio)
    {
        return GetClip(audio.Value);
    }

    private static AudioClip GetClip(string audio)
    {
        return Resources.Load<AudioClip>($"Audio/{audio}");
    }

    public static GameObject GetPlayer()
    {
        return Resources.Load<GameObject>($"Prefab/Player/Player");
    }
}
