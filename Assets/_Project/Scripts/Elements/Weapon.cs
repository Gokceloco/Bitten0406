using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Player player;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float attackRate;

    private float _lastAttackTime;

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed 
            && Time.time - _lastAttackTime > attackRate
            && player.gameDirector.gameState == GameState.GamePlay)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _lastAttackTime = Time.time;
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = transform.position + transform.forward;
        newBullet.transform.LookAt(transform.position + transform.forward * 2);
    }
    
}
