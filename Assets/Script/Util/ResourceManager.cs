using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

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
}
