using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CheckPointController : MonoBehaviour
{
    [SerializeField] public SpawnPointLocation GoToCheckPoint;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject)) {
            MainClass.Player.SetCheckPoint(GoToCheckPoint);
        }
    }
}
