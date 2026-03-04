using UnityEngine.Events;

public class CustomEvents
{
    public UnityEvent OnLanguageChanged = new UnityEvent();

    //Game
    public UnityEvent OnPauseGame = new UnityEvent();
    public UnityEvent OnResumeGame = new UnityEvent();
    public UnityEvent OnLivesChanged = new UnityEvent();
    public UnityEvent<int> OnPointsChanged = new UnityEvent<int>();
    public UnityEvent<int> OnCoinChanged = new UnityEvent<int>();
    public UnityEvent OnPlayerDeath = new UnityEvent();
    public UnityEvent OnPlayerDestroy = new UnityEvent();

    public UnityEvent<PlayerStatus> OnStatusPlayerChange = new UnityEvent<PlayerStatus>();
    public UnityEvent<bool> OnIsInvincibleChange = new UnityEvent<bool>();
    public UnityEvent<bool> OnIsVulnerabilityChange = new UnityEvent<bool>();
}
