using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
public class GoombaController : EnemyBase, IActivable
{
    private Rigidbody2D _rb;
    private bool startSleeping = true;

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
}
