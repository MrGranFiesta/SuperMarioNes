using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
