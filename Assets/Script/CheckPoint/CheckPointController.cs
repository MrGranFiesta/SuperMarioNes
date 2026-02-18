using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Vector3 Children;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagsUtils.IsPlayer(collision.gameObject)) {
            MainClass.Player.SetCheckPoint(Children);
        }
    }
}
