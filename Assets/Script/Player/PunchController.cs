using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PunchController : MonoBehaviour
{
    private BoxCollider2D _col;
    public void Awake()
    {
        _col = GetComponent<BoxCollider2D>();
        MainClass.CustomEvents.OnStatusPlayerChange.AddListener((newStatus) =>
        {
            UpdateColliderByStatus(newStatus);
        });
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IHittable>(out var hittable)) {
            if (transform.position.y < collision.bounds.min.y)
            {
                hittable.OnHit();
            }
        }
    }

    private void UpdateColliderByStatus(PlayerStatus status)
    {
        switch (status)
        {
            case PlayerStatus.Small:
                _col.offset = new Vector2(0, 0);
                break;
            case PlayerStatus.Big:
                _col.offset = new Vector2(0, 2.5f);
                break;
            case PlayerStatus.Fire:
                _col.offset = new Vector2(0, 2.5f);
                break;
        }
    }
}

