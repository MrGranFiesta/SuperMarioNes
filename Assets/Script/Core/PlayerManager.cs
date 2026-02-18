using UnityEngine;

public class PlayerManager
{
    public Vector3 CheckPoint { get; private set; }
    public PlayerStatus Status { get; private set; } = PlayerStatus.Small;
    public int Coins { get; private set; }
    public int Points { get; private set; }
    public int Live { get; private set; }
    public float TimeElapsed { get; private set; }
    public bool IsInvincible { get; private set; }
    public bool IsVulnerable { get; private set; }

    public void SetCheckPoint(Vector3 position) {
        CheckPoint = position;
    }

    public void PlusCoins() {
        Coins++;
        if (Coins >= 100)
        {
            Coins = 0;
            PlusLive();
        }
        MainClass.CustomEvents.OnCoinChanged.Invoke(Coins);
    }

    public void PlusLive()
    {
        if (Live <= 99)
        {
            Live++;
        }
    }

    public void MinusLive()
    {
        if (Live > 0)
        {
            Live--;
        }
    }

    public void PlusPoint(int point)
    {
        Points += point;
        MainClass.CustomEvents.OnPointsChanged.Invoke(Points);
    }

    public void SetStatusPlayer(PlayerStatus status) {
        Status = status;
        MainClass.CustomEvents.OnStatusPlayerChange?.Invoke(status);
    }

    public void SetIsInvincible(bool isInvincible) { 
        IsInvincible = isInvincible;
        MainClass.CustomEvents.OnIsInvincibleChange?.Invoke(isInvincible);
    }

    public void SetIsVulnerable(bool isVulnerable)
    {
        IsVulnerable = isVulnerable;
        MainClass.CustomEvents.OnIsVulnerabilityChange?.Invoke(isVulnerable);
    }
}
