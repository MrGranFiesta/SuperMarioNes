using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
public abstract class AutoMove : MonoBehaviour
{
    private Rigidbody2D _rig;
    protected SpriteRenderer _spriteRenderer;
 
    [SerializeField] protected float _speed = 3f;
    protected float _direction { get; set; } = 1f;

    [SerializeField] private float _rayDistance = 0.7f;
    private float _rayOffset = 0.25f;

    [SerializeField] protected LayerMask _layerMask;
    public bool isMove = true;

    protected virtual void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void SetLayerMask(LayerMask layerMask)
    {
        _layerMask = layerMask;
    }

    protected virtual void FixedUpdate()
    {
        Debug.Log("isMove " + isMove + " " + _direction + " " + _speed);
        if (isMove)
        {
            _rig.velocity = new Vector2(_direction * _speed, _rig.velocity.y);
        }
    }

    protected virtual void Update()
    {
        Vector2 directionRay = Vector2.zero;
        if (_direction == 1f)
        {
            directionRay = Vector2.right;
        }
        else if (_direction == -1f)
        {
            directionRay = Vector2.left;
        }

        RaycastHit2D hitLeft = Physics2D.Raycast(
                GetCenterPosition(),
                directionRay,
                _rayDistance,
                _layerMask
        );

        Debug.DrawRay(GetCenterPosition(), directionRay * _rayDistance, Color.red);

        if (hitLeft.collider != null)
        {
            Rotate();
        }
    }

    public virtual void StopMove()
    {
        isMove = false;
    }

    public virtual void PlayMove()
    {
        isMove = true;
    }

    private void Rotate()
    {
        _direction *= -1;
        FixSpriteRotation();
    }

    protected void FixSpriteRotation()
    {
        if (_direction == 1)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_direction == -1)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private Vector2 GetCenterPosition()
    {
        return (Vector2)transform.position + Vector2.down * _rayOffset;
    }
}
