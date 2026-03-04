using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(AudioListener))]
public class CamaraManager : MonoBehaviour
{
    private Transform Player;
    private float offsetX = 8f;

    public void Start()
    {
        Player = FindAnyObjectByType<PlayerController>().transform;
    }

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
