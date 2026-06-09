using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Player _player;

    [SerializeField] private int startHealth;
    private int _currentHealth;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private NavMeshAgent agent;

    public void StartEnemy()
    {
        _currentHealth = startHealth;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        agent.SetDestination(_player.transform.position);
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        healthBar.SetFillBar(startHealth, _currentHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
