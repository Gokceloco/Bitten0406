using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;

    private Vector3 _dir;
    private Rigidbody _rb;

    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask lookLayerMask;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void RestartPlayer()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        MovePlayer();
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hit, 50, lookLayerMask))
        {
            transform.LookAt(hit.point);
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


        if (Keyboard.current.leftShiftKey.isPressed)
        {
            _rb.linearVelocity = _dir * runSpeed + Vector3.up * yVelocity;
        }
        else
        {
            _rb.linearVelocity = _dir * speed + Vector3.up * yVelocity;
        }
    }
}
