using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public enum ActionState
{
    Idle,
    Walk,
    Attack,
}

public class Enemy : MonoBehaviour
{
    private Player _player;

    [SerializeField] private int startHealth;
    private int _currentHealth;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private NavMeshAgent agent;

    private ActionState _actionState;

    [SerializeField] private float attackDistance;

    private bool _isAttacking;

    public void StartEnemy()
    {
        _currentHealth = startHealth;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        //Decision
        var distance = (_player.transform.position - transform.position).magnitude;
        if (distance < attackDistance)
        {
            _actionState = ActionState.Attack;
        }
        else if (distance < 10 && !_isAttacking)
        {
            _actionState = ActionState.Walk;
        }

        //Action
        if (_actionState == ActionState.Walk)
        {
            agent.isStopped = false;
            agent.SetDestination(_player.transform.position);
        }
        else if (_actionState == ActionState.Attack && !_isAttacking)
        {
            agent.isStopped = true;
            _isAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
        else
        {
            agent.isStopped = true;
        }
    }

    IEnumerator AttackCoroutine()
    {
        transform.LookAt(_player.transform.position);

        yield return new WaitForSeconds(2);

        var forwardVector = transform.forward;
        var playerVector = _player.transform.position - transform.position;

        if ((_player.transform.position - transform.position).magnitude < 2
            && Vector3.Angle(forwardVector, playerVector) < 45)
        {
            _player.GetHit(1);
        }

        _isAttacking = false;
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

