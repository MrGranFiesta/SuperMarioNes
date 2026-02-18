using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FireController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rig;
    private bool _isMove = true;
    private float _speedX = 8f;
    private float _speedY = 3f;
    private float _directionX = 1f;
    private float _directionY = 1f;
    private float _rayDistanceHeightMax = 1.1f;
    private float _rayDistanceHeightMin = 0.4f;
    private LayerMask _layerMask;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
        _layerMask = LayerMask.GetMask(
            LayerUtils.Ground,
            LayerUtils.Block
        );
    }

    public void SetDirectionX(int directionX)
    {
        _directionX = directionX;
    }
    private void FixedUpdate()
    {
        if (_isMove)
        {
            _rig.velocity = new Vector2(_directionX * _speedX, _directionY * _speedY);
        }

        Vector2 centerHeightMax = new Vector2(
            transform.position.x + 0.3f,
            transform.position.y
        );

        Vector2 centerHeightMin = new Vector2(
            transform.position.x - 0.3f,
            transform.position.y
        );

        Debug.DrawRay(centerHeightMax, Vector2.down * _rayDistanceHeightMax, Color.green);
        Debug.DrawRay(centerHeightMin, Vector2.down * _rayDistanceHeightMin, Color.blue);

        RaycastHit2D HeightMax = Physics2D.Raycast(
            centerHeightMax,
            Vector2.down,
            _rayDistanceHeightMax,
            _layerMask
        );
        RaycastHit2D HeightMin = Physics2D.Raycast(
            centerHeightMin,
            Vector2.down,
            _rayDistanceHeightMin,
            _layerMask
        );

        if (HeightMax.collider == null)
        {
            _directionY = -1;
        }

        if (HeightMin.collider != null)
        {
            _directionY = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TagsUtils.IsEnemy(collision.gameObject))
        {
            Destroy(collision.gameObject);
            StartCoroutine(OnDisableByAnim());
            return;
            
        }

        ContactPoint2D contact = collision.GetContact(0);
        Vector2 normal = contact.normal;

        if (Mathf.Abs(normal.x) > 0.5f)
        {
            StartCoroutine(OnDisableByAnim());
        }
    }

    private void OnDisable()
    {
        _isMove = true;
        _rig.bodyType = RigidbodyType2D.Dynamic;
    }

    private IEnumerator OnDisableByAnim()
    {
        _isMove = false;
        _rig.bodyType = RigidbodyType2D.Static;
        _animator.SetTrigger(AnimationConst.OnExplote);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
