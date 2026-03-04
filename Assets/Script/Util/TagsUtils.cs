using UnityEngine;

public static class TagsUtils
{
    public const string Default = "Default";
    public const string Ground = "Ground";
    public const string Water = "Water";
    public const string Block = "Block";
    public const string TransparentFX = "TransparentFX";
    public const string Ignore_RayCast = "Ignore RayCast";
    public const string UI = "UI";
    public const string Player = "Player";
    public const string Enemy = "Enemy";

    public static bool IsPlayer(GameObject go)
    {
        return go.transform.CompareTag(Player);
    }

    public static bool IsPlayerAndNotDamable(GameObject go)
    {
        return go.transform.CompareTag(Player) && !(MainClass.Player.IsInvincible || MainClass.Player.IsVulnerable);
    }

    public static bool IsEnemy(GameObject go)
    {
        return go.transform.CompareTag(Enemy);
    }

    public static bool IsGround(GameObject go)
    {
        return go.transform.CompareTag(Ground);
    }
}
