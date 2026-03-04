using System.Collections;
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
            SoundConst.Stompswim.Play();
            OnChangeStatus.Invoke(KoopaStatus.ShieldInvulnerableState);
            _animator.SetTrigger(AnimationConst.OnStomp);
            _koopaController.StopMove();
            StartCoroutine(WaitTryGetUp());
        }
        else if(_koopaController.Status.IsShieldNotInvulnerable() && _koopaController.isMove) {
            SoundConst.Stompswim.Play();
            StopAllCoroutines();
            CancelTryGetUp(KoopaStatus.ShieldInvulnerableState);

            _koopaController.StopMove();
            StartCoroutine(WaitTryGetUp());
        } 
        else if (_koopaController.Status.IsShieldNotInvulnerable() && !_koopaController.isMove) {
            SoundConst.KickKill.Play();
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
        yield return new WaitForSeconds(GameConstants.DelayInvulnerability);
        OnChangeStatus.Invoke(KoopaStatus.ShieldState);
        yield return new WaitForSeconds(GameConstants.DelayTryGetUpState);
        OnChangeStatus.Invoke(KoopaStatus.ShieldTryGetUpState);
        _animator.SetTrigger(AnimationConst.OnTryGetUp);
        yield return new WaitForSeconds(GameConstants.DelayGetUp);
        _animator.SetTrigger(AnimationConst.OnGetUp);
        _koopaController.PlayMove();
        OnChangeStatus.Invoke(KoopaStatus.WalkingState);
    }
}
