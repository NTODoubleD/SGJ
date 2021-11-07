using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public enum EnemyStates
{
    idle,
    attackPlayer
}

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    protected EnemyStates _state;
    protected NavMeshAgent _agent;
    [SerializeField] protected Weapon _weapon;
    protected Rigidbody _rigidbody;

    protected bool _isDead = false;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void StopAgentByTime(float seconds)
    {
        if (_agent.enabled)
            StartCoroutine(StopAgent(seconds));
    }

    private IEnumerator StopAgent(float seconds)
    {
        if (_agent != null)
            _agent.isStopped = true;
        _rigidbody.isKinematic = false;
        yield return new WaitForSeconds(seconds);
        if (_agent.isOnNavMesh)
            _agent.isStopped = false;
        _rigidbody.isKinematic = true;


    }

    public virtual void Damage()
    {
        ImperialClass.Instance?.SetState(ImperialStates.HuntingPlayer);
    }

    public void Die()
    {
        _isDead = true;
    }
}
