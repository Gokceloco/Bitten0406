using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector3 _dir;
    private Rigidbody _rb;
    public float speed;

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
        _rb.linearVelocity = new Vector3(_dir.x, 0, _dir.z) * speed;
    }
}
