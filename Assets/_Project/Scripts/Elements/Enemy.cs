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

    [SerializeField] private Animator animator;

    private AnimationState _currentAnimationState;

    private bool _didSeePlayer;

    private bool _isDead;

    [SerializeField] private Collider col;

    public void StartEnemy()
    {
        _currentHealth = startHealth;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (_isDead)
        {
            agent.isStopped = true;
            return;
        }
        //Decision
        var distance = (_player.transform.position - transform.position).magnitude;
        if (distance < attackDistance)
        {
            _actionState = ActionState.Attack;
        }
        else if (distance < 10 && !_isAttacking)
        {
            _didSeePlayer = true;
            _actionState = ActionState.Walk;
        }
        else if (!_isAttacking)
        {
            if (_didSeePlayer)
            {
                _actionState = ActionState.Walk;
            }
            else
            {
                _actionState = ActionState.Idle;
            }
        }

        //Action
        if (_actionState == ActionState.Walk)
        {
            agent.isStopped = false;
            agent.SetDestination(_player.transform.position);
            ChangeAnimationState(AnimationState.Walk);
        }
        else if (_actionState == ActionState.Attack && !_isAttacking)
        {
            agent.isStopped = true;
            _isAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
        else if (!_isAttacking)
        {
            ChangeAnimationState(AnimationState.Idle);
            agent.isStopped = true;
        }
    }

    void ChangeAnimationState(AnimationState desiredState)
    {
        if (desiredState != _currentAnimationState)
        {
            _currentAnimationState = desiredState;
            animator.Play(desiredState.ToString());
        }
    }

    IEnumerator AttackCoroutine()
    {
        animator.CrossFade("Attack", .25f, -1, 0, 0);
        
        _currentAnimationState = AnimationState.Attack;
        transform.LookAt(_player.transform.position);

        yield return new WaitForSeconds(1.75f);

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
        col.enabled = false;
        _isDead = true;
        ChangeAnimationState(AnimationState.Die);
        Destroy(gameObject, 3);
    }
}

public enum AnimationState
{
    Idle,
    Walk,
    Attack,
    Die
}