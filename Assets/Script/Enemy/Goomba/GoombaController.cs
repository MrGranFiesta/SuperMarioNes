using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
public class GoombaController : EnemyBase, IActivable
{
    private Rigidbody2D _rb;
    private bool startSleeping = true;
    [NonSerialized] public bool IsDeath;

    protected override void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        base.Awake();
        OnInnative();
        StopMove();
    }
    public void OnActivate()
    {
        if (startSleeping) {
            startSleeping = false;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            PlayMove();
        }
    }

    public void OnInnative()
    {
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsDeath)
        {
            base.OnCollisionEnter2D(collision);
        }
    }

    public void OnDeath()
    {
        IsDeath = true;
        gameObject.layer = LayerMask.NameToLayer(LayerUtils.Inactive);
    }
}
