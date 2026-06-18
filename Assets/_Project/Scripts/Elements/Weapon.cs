using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public Player player;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float attackRate;

    private float _lastAttackTime;

    [SerializeField] private ParticleSystem muzzlePS;
    [SerializeField] private Light muzzleLight;

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
        var newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
        newBullet.StartBullet(player.gameDirector);
        newBullet.transform.LookAt(transform.position + transform.forward * 2);
        muzzlePS.Play();

        muzzleLight.DOKill();
        muzzleLight.intensity = 0;
        muzzleLight.DOIntensity(50, 0.05f).SetLoops(2, LoopType.Yoyo);

        player.gameDirector.audioManager.PlayShootAS();
    }
    
}
