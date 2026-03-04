using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameObject PlayerCamera;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TagsUtils.IsPlayer(collision.gameObject))
        {
            Destroy(collision.gameObject);
            return;
        }
        MainClass.Player.MinusLive();
        MainClass.CustomEvents.OnPlayerDeath.Invoke();
        SoundConst.Death.Play();
        Destroy(collision.gameObject);
    }
}
