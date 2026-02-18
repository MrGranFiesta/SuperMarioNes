using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer), typeof(Animator))]
public abstract class BlockBase : MonoBehaviour, IHittable
{
    protected float OffsetY = 0.3f;
    protected float Velocity = 6f;

    protected Vector3 OriginTarget;
    protected Vector3 PositionTarget;
    protected bool IsActive = false;

    protected Collider2D Col;
    protected SpriteRenderer Sprite;
    protected Animator Animator;

    protected virtual void Awake()
    {
        Col = GetComponent<Collider2D>();
        Sprite = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        PositionTarget = transform.position + Vector3.up * OffsetY;
        OriginTarget = transform.position;
    }

    public virtual void OnHit()
    {
        OnHit(false);
    }

    public virtual void OnHit(
        bool isLastHit = false
    )
    {
        if (!IsActive)
        {
            IsActive = true;
            if (isLastHit)
            {
                Animator.SetBool(AnimationConst.IsHit, true);
            }
            StartCoroutine(HitAnimation());
            IsActive = false;
        }
    }

    private IEnumerator HitAnimation()
    {
        while (transform.position != PositionTarget)
        {
            MoveTowards(PositionTarget);
            yield return null;
        }

        while (transform.position != OriginTarget)
        {
            MoveTowards(OriginTarget);
            yield return null;
        }
    }

    private void MoveTowards(Vector3 posTarget)
    {
        AnimationUtils.MoveTowards(
            gameObject,
            transform.position,
            posTarget,
            Velocity
        );
    }

    public void LaunchCoinAnimation()
    {
        GameObject go = ResourceManager.GetCoinAnimation();
        Vector3 spawnPos = new Vector3(
            transform.position.x,
            Col.bounds.max.y + Col.bounds.size.x / 1.5f,
            transform.position.z
        );
        GameObject coin = Instantiate(go, spawnPos, Quaternion.identity);
        MainClass.Player.PlusCoins();
        MainClass.Player.PlusPoint(PointsUtils.Coin);
    }

    protected void GeneratePowerUp(PowerUp content)
    {
        GameObject go = ResourceManager.GetPrefabItem(content);
        Vector3 spawnPos = new Vector3(
            transform.position.x ,
            Col.bounds.max.y + Col.bounds.size.y / 2,
            transform.position.z
        );
        Instantiate(go, spawnPos, Quaternion.identity);
    }
}
