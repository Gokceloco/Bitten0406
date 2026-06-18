using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 _spawnPosition;

    private GameDirector _gameDirector;

    public void StartBullet(GameDirector gameDirector)
    {
        _spawnPosition = transform.position;
        _gameDirector = gameDirector;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        var distance = (transform.position - _spawnPosition).magnitude;
        if (distance > 25)
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DestroyBullet();
            collision.gameObject.GetComponent<Enemy>().GetHit(1);
            _gameDirector.fXManager.PlayImpactPS(transform.position, transform.forward);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
