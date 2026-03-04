using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GoombaHeaderController : MonoBehaviour, IStompable
{
    private Animator _animator;
    private GoombaController _goombaController;
    private CapsuleCollider2D _col;
    private Rigidbody2D _rb;
    
    public void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        _goombaController = GetComponentInParent<GoombaController>();
        _col = GetComponentInParent<CapsuleCollider2D>();
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    public void OnStomp()
    {
        if (!_goombaController.IsDeath)
        {
            SoundConst.Stompswim.Play();
            gameObject.layer = LayerMask.NameToLayer(LayerUtils.Inactive);
            _goombaController.OnDeath();
            _animator.SetBool(AnimationConst.IsDeath, true);
            _goombaController.StopMove();
            _rb.bodyType = RigidbodyType2D.Static;
            _col.enabled = false;
            StartCoroutine(DestroyParentGameObject());
        }
    }

    private IEnumerator DestroyParentGameObject() {
        yield return new WaitForSeconds(GameConstants.DelayDestroyGoomba);
        Destroy(transform.parent.gameObject);
        yield return null;
    }
}
