using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D), typeof(Animator))]
public class KoopaController : EnemyBase, IActivable
{
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    private KoopaHeaderController _headerController;
    private bool startSleeping = true;

    [NonSerialized] public KoopaStatus Status = KoopaStatus.WalkingState;

    private LayerMask WalkingMode;
    private LayerMask ShieldMode;

    protected override void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        base.Awake();
        _col = GetComponent<CapsuleCollider2D>();
        WalkingMode = LayerMask.GetMask(
            LayerUtils.Default,
            LayerUtils.Ground,
            LayerUtils.Block,
            LayerUtils.Enemy
        );
        ShieldMode = LayerMask.GetMask(
            LayerUtils.Default,
            LayerUtils.Ground,
            LayerUtils.Block
        );
        SetLayerMask(WalkingMode);
        _headerController = GetComponentInChildren<KoopaHeaderController>();
        _headerController.OnChangeStatus.AddListener((newStatus) =>
            UpdateStatus(newStatus)
        );

        OnInnative();
        StopMove();
    }

    private void UpdateStatus(KoopaStatus newStatus)
    {
        switch (newStatus)
        {
            case KoopaStatus.Walking:
                _col.size = new Vector2(1, 1.5f);
                _col.offset = new Vector2(0f, -0.25f);
                SetSpeed(3f);
                SetLayerMask(WalkingMode);
                break;
            case KoopaStatus.Shield:
                UpdateStatusShield();
                break;
            case KoopaStatus.ShieldInvulnerable:
                UpdateStatusShield();
                break;
        }

        Status = newStatus;
    }

    private void UpdateStatusShield() {
        _col.size = new Vector2(0.9f, 0.8f);
        _col.offset = new Vector2(0f, 0f);
        SetLayerMask(ShieldMode);
        SetSpeed(6f);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (Status.IsShieldInvulnerable())
        {
            return;    
        }

        if (TagsUtils.IsPlayerAndNotDamable(collision.gameObject))
        {
            OnCollisionEnterPlayer(collision);
        }

        if (TagsUtils.IsEnemy(collision.gameObject) && (Status.IsOnlyShield() && isMove) )
        {
            Destroy(collision.gameObject);
            //TODO ANIMACION ROTAR Y CAER
        }
    }

    private void OnCollisionEnterPlayer(Collision2D collision)
    {
        if (Status.IsWalking())
        {
            Damage();
        }
        else if (Status.IsShieldNotInvulnerable() && !isMove)
        {
            LaunchShield(collision);
        }
        else if (Status.IsOnlyShield() && isMove)
        {
            Damage();
        }
    }

    public void LaunchShield(Collision2D collision) {
        _headerController.StopAllCoroutines();
        _headerController.CancelTryGetUp(KoopaStatus.ShieldState);

        RotationByCollision(collision);
        SetSpeed(6f);
        PlayMove();
    }

    private void RotationByCollision(Collision2D collision) {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 normal = contact.normal;

        if(normal.x > 0) {
            _direction = 1f;
        } else if (normal.x < 0) {
            _direction = -1f;
        }
    }

    public void OnActivate()
    {
        if (startSleeping)
        {
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
