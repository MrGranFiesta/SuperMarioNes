
public class PlayerManager
{
    public SpawnPointLocation? CheckPoint { get; private set; } = SpawnPointLocation.Check_1_1_A;
    public SpawnPointLocation? SpawnPointTemporaly { get; private set; } = null;
    public PlayerStatus Status { get; private set; } = PlayerStatus.Small;
    public int Coins { get; private set; }
    public int Points { get; private set; }
    public int Live { get; private set; }
    public float TimeElapsed { get; private set; } = GameConstants.TimerElapsedStarted;
    public bool IsInvincible { get; private set; }
    public bool IsVulnerable { get; private set; }

    public bool IsDamage()
    {
        return !IsInvincible || !IsVulnerable;
    }

    public void SetCheckPoint(SpawnPointLocation position) {
        CheckPoint = position;
    }

    public void SetSpawnPointTemporaly(SpawnPointLocation position)
    {
        SpawnPointTemporaly = position;
    }

    public SpawnPointLocation? GetSpawnPointTemporalyAndRemove() {
        SpawnPointLocation? location = SpawnPointTemporaly;
        SpawnPointTemporaly = null;
        return location;
    }

    public void RemoveSpawnPointTemporaly()
    {
        SpawnPointTemporaly = null;
    }

    public void PlusCoins() {
        Coins++;
        SoundConst.Coin.Play();
        if (Coins >= 100)
        {
            Coins = 0;
            PlusLive();
        }
        MainClass.CustomEvents.OnCoinChanged.Invoke(Coins);
    }

    public void PlusLive()
    {
        SoundConst.LifeUp.Play();
        if (Live <= 99)
        {
            Live++;
        }
    }

    public void SetLive(int live)
    {
        Live = live;
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

    public void SetTimeElapsed(float time) {
        TimeElapsed = time;
    }

    public void MinusTimeElapsed() {
        TimeElapsed--;
    }

    public void Reset() {
        CheckPoint = SpawnPointLocation.Check_1_1_A;
        SpawnPointTemporaly = null;
        Status = PlayerStatus.Small;
        Coins = 0;
        Points = 0;
        Live = MainClass.Config.Dificult.LivesStarter;
        TimeElapsed = GameConstants.TimerElapsedStarted;
        IsInvincible = false;
        IsVulnerable = false;
    }
}
