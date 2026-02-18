using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class KoopaHeaderController : MonoBehaviour, IStompable
{
    private Animator _animator;
    private KoopaController _koopaController;
    public UnityEvent<KoopaStatus> OnChangeStatus = new UnityEvent<KoopaStatus>();

    public void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        _koopaController = GetComponentInParent<KoopaController>();
    }

    public void OnStomp()
    {
        if (_koopaController.Status.IsWalking())
        {
            OnChangeStatus.Invoke(KoopaStatus.ShieldInvulnerableState);
            _animator.SetTrigger(AnimationConst.OnStomp);
            _koopaController.StopMove();
            StartCoroutine(WaitTryGetUp());
        }
        else if(_koopaController.Status.IsShieldNotInvulnerable() && _koopaController.isMove) {
            StopAllCoroutines();
            CancelTryGetUp(KoopaStatus.ShieldInvulnerableState);

            _koopaController.StopMove();
            StartCoroutine(WaitTryGetUp());
        } 
        else if (_koopaController.Status.IsShieldNotInvulnerable() && !_koopaController.isMove) {
            StopAllCoroutines();
            CancelTryGetUp(KoopaStatus.ShieldState);

            _koopaController.PlayMove();
        }
    }

    public void CancelTryGetUp(KoopaStatus SetStatus) {
        if (_koopaController.Status.IsShieldTryGetUp())
        {
            OnChangeStatus.Invoke(SetStatus);
            _animator.SetTrigger(AnimationConst.OnStomp);
        }
    }

    private IEnumerator WaitTryGetUp()
    {
        yield return new WaitForSeconds(0.2f);
        OnChangeStatus.Invoke(KoopaStatus.ShieldState);
        yield return new WaitForSeconds(4);
        OnChangeStatus.Invoke(KoopaStatus.ShieldTryGetUpState);
        _animator.SetTrigger(AnimationConst.OnTryGetUp);
        yield return new WaitForSeconds(3);
        _animator.SetTrigger(AnimationConst.OnGetUp);
        _koopaController.PlayMove();
        OnChangeStatus.Invoke(KoopaStatus.WalkingState);
    }
}
