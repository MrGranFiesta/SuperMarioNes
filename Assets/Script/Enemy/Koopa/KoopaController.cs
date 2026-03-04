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
    private int combo = 1;

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
        _headerController.OnChangeStatus.AddListener((newStatus) => {
            combo = 1;
            UpdateStatus(newStatus);
        });

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
                SetSpeed(GameConstants.MaxVelocityXKoopaWalking);
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
        SetSpeed(GameConstants.MaxVelocityXKoopaWalking);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (Status.IsShieldInvulnerable())
        {
            return;    
        }

        if (TagsUtils.IsPlayerAndNotDamable(collision.gameObject))
        {
            SoundConst.KickKill.Play();
            OnCollisionEnterPlayer(collision);
        }

        if (TagsUtils.IsEnemy(collision.gameObject) && (Status.IsOnlyShield() && isMove) )
        {
            SoundConst.KickKill.Play();
            Destroy(collision.gameObject);
            int point = PointsUtils.GetPointByCombo(combo);
            if (point == 0)
            {
                MainClass.Player.PlusLive();
            }
            MainClass.Player.PlusPoint(point);
            combo++;
        }
    }

    private void OnCollisionEnterPlayer(Collision2D collision)
    {
        if (Status.IsWalking())
        {
            Damage(collision);
        }
        else if (Status.IsShieldNotInvulnerable() && !isMove)
        {
            LaunchShield(collision);
        }
        else if (Status.IsOnlyShield() && isMove)
        {
            Damage(collision);
        }
    }

    public void LaunchShield(Collision2D collision) {
        _headerController.StopAllCoroutines();
        _headerController.CancelTryGetUp(KoopaStatus.ShieldState);

        RotationByCollision(collision);
        SetSpeed(GameConstants.MaxVelocityXKoopaShield);
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
