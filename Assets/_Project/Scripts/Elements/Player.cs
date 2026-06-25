using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region References
    public GameDirector gameDirector;

    private Vector3 _dir;
    private Rigidbody _rb;

    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask lookLayerMask;
    [SerializeField] private LayerMask jumpLayerMask;

    [SerializeField] private float jumpPower;

    [SerializeField] private int startHealth;
    private int _currentHealth;
    [SerializeField] private HealthBar healthBar;

    public Light spotLight;

    [SerializeField] private Animator animator;
    #endregion

    private PlayerAnimationState _currentAnimationState;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void RestartPlayer()
    {
        gameObject.SetActive(true);
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.position = Vector3.zero;
        _currentHealth = startHealth;
    }
    private void Update()
    {
        if (gameDirector.gameState != GameState.GamePlay)
        {
            _rb.linearVelocity = Vector3.zero;
            if (gameDirector.gameState == GameState.WinUI)
            {
                ChangeAnimationState(PlayerAnimationState.Idle);
            }
            return;
        }
        MovePlayer();
        LookAtMouse();
        Jump();
        if (transform.position.y < -10)
        {
            gameDirector.LevelFailed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            gameDirector.LevelCompleted();
            other.gameObject.SetActive(false);
        }
    }
    public void GetHit(int damage)
    {
        if (gameDirector.gameState != GameState.GamePlay)
        {
            return;
        }
        _currentHealth -= damage;
        healthBar.SetFillBar(startHealth, _currentHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {        
        gameDirector.LevelFailed();
        ChangeAnimationState(PlayerAnimationState.Die);
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * .5f, Vector3.down, 1, jumpLayerMask);        
    }
    private void Jump()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && IsGrounded())
        {
            _rb.AddForce(Vector3.up * jumpPower);
        }
    }
    private void LookAtMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hit, 50, lookLayerMask))
        {
            var lookPos = hit.point;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
        }
    }
    private void MovePlayer()
    {
        _dir = Vector3.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            _dir += Vector3.forward;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            _dir += Vector3.back;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            _dir += Vector3.left;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            _dir += Vector3.right;
        }
        _dir = _dir.normalized;

        var yVelocity = _rb.linearVelocity.y;

        if (_dir == Vector3.zero)
        {
            ChangeAnimationState(PlayerAnimationState.Idle);
        }
        else
        {
            ChangeAnimationState(PlayerAnimationState.Run);
        }

        if (Keyboard.current.leftShiftKey.isPressed)
        {
            _rb.linearVelocity = _dir * runSpeed + Vector3.up * yVelocity;
        }
        else
        {
            _rb.linearVelocity = _dir * speed + Vector3.up * yVelocity;
        }

        var angle = Vector3.SignedAngle(_dir, transform.forward, Vector3.up);
        animator.SetFloat("Blend", angle);
    }
    public void ChangeAnimationState(PlayerAnimationState key)
    {
        if (_currentAnimationState != key)
        {
            _currentAnimationState = key;
            animator.SetTrigger(key.ToString());
        }
    }
}

public enum PlayerAnimationState
{
    Idle,
    Run,
    Die,
}