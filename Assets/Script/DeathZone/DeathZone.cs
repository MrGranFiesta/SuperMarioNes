using System.Collections;
using System.Collections.Generic;
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

        Vector3 respauwnPoint = MainClass.Player.CheckPoint; 
        MainClass.Player.MinusLive();

        PlayerCamera.gameObject.transform.position = new Vector3(respauwnPoint.x - 5, 7f, -1f);
        collision.transform.position = respauwnPoint;
    }
}
