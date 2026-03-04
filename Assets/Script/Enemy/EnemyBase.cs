using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
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
            Damage(collision);
        }
    }

    protected void Damage(Collision2D collision) {
        if (MainClass.Player.Status != PlayerStatus.Small && MainClass.Player.IsDamage())
        {
            MainClass.Player.SetStatusPlayer(PlayerStatus.Small);
            MainClass.Player.SetIsVulnerable(true);
        }
        else
        {
            collision.gameObject.GetComponent<PlayerController>()?.DeathPlayer();
        }
    }


    public void SetSpeed(float velocity)
    {
        _speed = velocity;
    }
}
