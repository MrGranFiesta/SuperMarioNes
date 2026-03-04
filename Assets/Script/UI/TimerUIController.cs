using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerUIController : MonoBehaviour
{
    private TextMeshProUGUI txt;
    private float accumulator = 0f;

    private const float NES_TICK = 24f / 60f;
    private bool isActiveHurryUp = false;
    [SerializeField] private bool isActive = true;
    private PlayerController player;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        FormatedTime();
        if (!isActive) {
            txt.text = "";
        }
    }

    private void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (!isActive) { return; }

        if (MainClass.Player.TimeElapsed <= 0) {
            player?.DeathPlayer();
            return; 
        }

        accumulator += Time.deltaTime;

        if (accumulator >= NES_TICK)
        {
            MainClass.Player.MinusTimeElapsed();
            accumulator -= NES_TICK;
        }

        FormatedTime();

        if (MainClass.Player.TimeElapsed < 100 && !isActiveHurryUp) {
            isActiveHurryUp = true;
            SoundConst.HurryUp.Play();
        }
    }

    private void FormatedTime() {
        txt.text = ((int)MainClass.Player.TimeElapsed).ToString("000");
    }
}
