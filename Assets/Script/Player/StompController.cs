using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StompController : MonoBehaviour
{
    private BoxCollider2D _col;
    private Rigidbody2D _rig;
    private int combo = 0;
    void Awake()
    {
        _col = GetComponent<BoxCollider2D>();
        _rig = GetComponentInParent<Rigidbody2D>();
        MainClass.CustomEvents.OnStatusPlayerChange.AddListener((newStatus) =>
        {
            UpdateColliderByStatus(newStatus);
        });
    }

    private void UpdateColliderByStatus(PlayerStatus status)
    {
        switch (status)
        {
            case PlayerStatus.Small:
                _col.offset = new Vector2(0, 0);
                break;
            case PlayerStatus.Big:
                _col.offset = new Vector2(0, -2.5f);
                break;
            case PlayerStatus.Fire:
                _col.offset = new Vector2(0, -2.5f);
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (TagsUtils.IsGround(collision.gameObject))
        {
            combo = 1;
        }

        if (collision.gameObject.TryGetComponent<IStompable>(out var stompable))
        {
            _rig.AddForce(Vector2.up * 25, ForceMode2D.Impulse);
            if (TagsUtils.IsEnemy(collision.gameObject))
            {
                int point = PointsUtils.GetPointByCombo(combo);
                if (point == 0)
                {
                    MainClass.Player.PlusLive();
                }

                MainClass.Player.PlusPoint(point);
                combo++;
            }
            stompable.OnStomp();
        }
    }

    private void GetPointByCombo()
    {

    }
}
