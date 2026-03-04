using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject))
        {
            MainClass.Player.PlusCoins();
            MainClass.Player.PlusPoint(PointsUtils.Coin);
            Destroy(gameObject);
        }
    }
}
