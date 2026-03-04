using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private bool _isGrounded = false;
    private LayerMask _layerMask;

    private float _jumpForce = GameConstants.JumpForcePlayer;
    private float _moveForce = GameConstants.MoveForcePlayer;
    private float _rayDistance = 0.1f;


    private Rigidbody2D _rig;
    private CapsuleCollider2D _col;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private Vector2 _moveInput;

    private PlayerStatus _status;

    private float _fireCooldown = GameConstants.CooldownDisableFire;
    private float _lastFireTime = -10f;
    private const int MaxVelocityX = GameConstants.MaxVelocityXPlayer;
    private const int MaxVelocityY = GameConstants.MaxVelocityYPlayer;

    private bool isDeath = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitialiceComponentes();
        InitializeData();
        CreateListener();
    }

    private void InitialiceComponentes()
    {
        _rig = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void InitializeData()
    {
        _layerMask = LayerMask.GetMask(
            LayerUtils.Block,
            LayerUtils.Default,
            LayerUtils.Ground,
            LayerUtils.Enemy
        );
        _status = MainClass.Player.Status;
        _animator.SetInteger(AnimationConst.Status, (int)_status);
        NotExecuteStartAnim();
        UpdateSpriteRendererByStatus();

    }

    private void NotExecuteStartAnim()
    {
        switch ((int)_status)
        {
            case 0:
                _animator.SetTrigger(AnimationConst.OnNotStartedAnimSmall);
                break;
            case 1:
                _animator.SetTrigger(AnimationConst.OnNotStartedAnimBig);
                break;
            case 2:
                _animator.SetTrigger(AnimationConst.OnNotStartedAnimFired);
                break;
        }
    }

    private void CreateListener() {
        MainClass.CustomEvents.OnStatusPlayerChange.AddListener((newStatus) =>
        {
            _status = newStatus;
            OnChangeStatusAnimation(newStatus);
            UpdateSpriteRendererByStatus();
        });

        MainClass.CustomEvents.OnIsVulnerabilityChange.AddListener((IsVulnerability) =>
        {
            if (IsVulnerability)
            {
                StartCoroutine(AnimationVulnerability());
            }
        });

        MainClass.CustomEvents.OnIsInvincibleChange.AddListener((IsInvincible) =>
        {
            _animator.SetBool(AnimationConst.IsInvulnerability, IsInvincible);
            if (IsInvincible)
            {
                StartCoroutine(TimeInvulnerability());
            }
        });

        InputManager.InputActions.Player.Jump.performed += HandleJump;
        InputManager.InputActions.Player.LaunchedFire.performed += LaunchedFire;
        MainClass.CustomEvents.OnPlayerDestroy.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }

    private void UpdateSpriteRendererByStatus()
    {
        switch (_status)
        {
            case PlayerStatus.Small:
                _col.size = new Vector2(0.8f, 1);
                break;
            case PlayerStatus.Big:
                _col.size = new Vector2(1, 2);
                break;
            case PlayerStatus.Fire:
                _col.size = new Vector2(1, 2);
                break;
        }
    }

    private IEnumerator AnimationVulnerability()
    {
        var vulnerabilityCooldown = Time.time + 3f;
        while (Time.time < vulnerabilityCooldown)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(GameConstants.BlinkingAnimBySecond);
        }
        _spriteRenderer.enabled = true;
        MainClass.Player.SetIsVulnerable(false);
    }

    private void RotationPlayer()
    {
        if (_moveInput.x > 0.1f) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (_moveInput.x < -0.1f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }

    private void Update()
    {
        _animator.SetFloat(AnimationConst.VelocityX, Mathf.Abs(_rig.velocity.x));
        Vector2 bottomLeft = new Vector2(
            _col.bounds.min.x,
            _col.bounds.min.y
        );

        Vector2 bottomRight = new Vector2(
            _col.bounds.max.x,
            _col.bounds.min.y
        );

        Debug.DrawRay(bottomLeft, Vector2.down * _rayDistance, Color.blue);
        Debug.DrawRay(bottomRight, Vector2.down * _rayDistance, Color.green);

        _isGrounded = IsGrounded();
        _animator.SetBool(AnimationConst.IsGrounded, _isGrounded);
        if (_rig.velocity.y > MaxVelocityY) {
            _rig.velocity = new Vector2(_rig.velocity.x, MaxVelocityY);
        }

        if (_rig.velocity.x > MaxVelocityX)
        {
            _rig.velocity = new Vector2(MaxVelocityX, _rig.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        _moveInput = InputManager.InputActions.Player.Move.ReadValue<Vector2>();
        if (Mathf.Abs(_moveInput.x) > 0.01f)
        {
            _rig.AddForce(_moveInput * _moveForce, ForceMode2D.Impulse);
        } else if (_isGrounded)
        {
            _rig.velocity -= new Vector2(1 * _rig.velocity.x, 0);
        }

        RotationPlayer();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        if (_isGrounded && !isDeath)
        {
            if (PlayerStatus.Small == MainClass.Player.Status)
            {
                SoundConst.JumpSmall.Play();
            } else
            {
                SoundConst.Jump.Play();

            }
            _rig.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse); 
        }
    }

    public void DeathPlayer()
    {
        if (isDeath) { return; }
        isDeath = true;
        MainClass.Player.MinusLive();
        if (MainClass.Player.Status == PlayerStatus.Small) {
            _animator.SetTrigger(AnimationConst.OnDeath);
            InputManager.InputActions.Disable();
            gameObject.layer = LayerMask.NameToLayer(LayerUtils.Inactive);
        }
        SoundConst.Death.Play();
        StartCoroutine(DestroyDelay());
    }


    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(GameConstants.DelayPlayerDeath);
        MainClass.CustomEvents.OnPlayerDeath.Invoke();
        Destroy(gameObject);
    }

    private void OnChangeStatusAnimation(PlayerStatus newStatus)
    {
        _animator.SetInteger(AnimationConst.Status, (int) newStatus);
        _animator.SetTrigger(AnimationConst.OnPlayerChanged);
    }

    private bool IsGrounded()
    {
        Vector2 bottomLeft = new Vector2(
            _col.bounds.min.x,
            _col.bounds.min.y
        );
        Vector2 bottomRight = new Vector2(
            _col.bounds.max.x,
            _col.bounds.min.y
        );

        RaycastHit2D hitLeft = Physics2D.Raycast(
            bottomLeft,
            Vector2.down,
            _rayDistance,
            _layerMask
        );
        RaycastHit2D hitRight = Physics2D.Raycast(
            bottomRight,
            Vector2.down,
            _rayDistance,
            _layerMask
        );

        return hitLeft.collider != null || hitRight.collider != null;
    }

    private IEnumerator TimeInvulnerability()
    {
        yield return new WaitForSeconds(GameConstants.TimeInvulnerabiliy);
        MainClass.Player.SetIsInvincible(false);
    }

    private void LaunchedFire(InputAction.CallbackContext context)
    {
        if (_status != PlayerStatus.Fire) { return; }

        if (Time.time < _lastFireTime + _fireCooldown)
        {
            return;
        }
        _lastFireTime = Time.time;

        
        _animator.SetTrigger(AnimationConst.OnLaunchFired);
        GameObject go = FirePooling.Instance.GetFireAndCreate(
            transform.position,
            GetDirectionByRotation()
        );
        go.transform.position = transform.position;
    }

    private int GetDirectionByRotation() {
        if (transform.eulerAngles.y == 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(TagsUtils.IsEnemy(collision.gameObject) && MainClass.Player.IsInvincible) { 
            SoundConst.KickKill.Play();
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        InputManager.InputActions.Player.Jump.performed -= HandleJump;
        InputManager.InputActions.Player.LaunchedFire.performed -= LaunchedFire;
    }
}
