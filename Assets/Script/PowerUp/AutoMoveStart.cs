using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class AutoMoveStart : MonoBehaviour
{
    private Rigidbody2D _rig;
    private float _directionX = 1f;
    private float _directionY = 1f;
    private float _speedX = 2.5f;
    private float _speedY = 5.5f;

    protected virtual void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(TagsUtils.IsPlayer(collision.gameObject)) { return; }

        if (collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            {
                _directionX *= -1f;
            }
        }

        _rig.velocity = new Vector2(_directionX * _speedX, _directionY * _speedY);
    }
}
