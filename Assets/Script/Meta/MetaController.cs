using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MetaController : MonoBehaviour
{
    [SerializeField] private Level NextLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject))
        {
            MainClass.Player.PlusPoint(PointsUtils.Meta);
            MainClass.Player.PlusPoint(PointsUtils.SecondPoint * (int) MainClass.Player.TimeElapsed);
            Destroy(collision.gameObject);
            MainClass.Datastore.AddPoints(MainClass.Player.Points);
            MainClass.Player.Reset();
            Destroy(collision.gameObject);
            NextLevel.LoadLevel();
        }
    }
}
