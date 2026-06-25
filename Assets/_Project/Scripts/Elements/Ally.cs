using UnityEngine;
using UnityEngine.AI;

public class Ally : MonoBehaviour
{
    public Player player;
    private NavMeshAgent _agent;
    private bool _isFollowing;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isFollowing)
        {
            _agent.isStopped = false;
            _agent.SetDestination(player.transform.position);
        }
        else
        {
            _agent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isFollowing = true;
        }
    }
}
