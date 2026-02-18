using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(AudioListener))]
public class CamaraManager : MonoBehaviour
{
    public Transform Player;
    private float offsetX = 8f;

    private void FixedUpdate()
    {
        if (Player == null) return;

        float posCamX = transform.position.x;
        float posPlayerX = Player.transform.position.x;

        if (posPlayerX > posCamX - offsetX) {
            transform.position = new Vector3(
                posPlayerX + offsetX,
                7f,
                -10f
            );
        }
    }
}
