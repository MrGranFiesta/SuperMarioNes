using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class EnemyBase : AutoMove
{

    protected override void Awake()
    {
        base.Awake();
        _direction = -1f;
        FixSpriteRotation();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(TagsUtils.IsPlayerAndNotDamable(collision.gameObject)) {
            Damage();
        }
    }

    protected void Damage() {
        if (MainClass.Player.Status != PlayerStatus.Small)
        {
            MainClass.Player.SetStatusPlayer(PlayerStatus.Small);
            MainClass.Player.SetIsVulnerable(true);
        }
        else
        {
            MainClass.Player.MinusLive();
            //TODO Death Player
        }
    }


    public void SetSpeed(float velocity)
    {
        _speed = velocity;
    }
}
