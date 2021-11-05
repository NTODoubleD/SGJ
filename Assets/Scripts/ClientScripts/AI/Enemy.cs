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

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void StopAgentByTime(float seconds)
    {
        if (_agent.enabled)
            StartCoroutine(StopAgent(seconds));
    }

    private IEnumerator StopAgent(float seconds)
    {
        _agent.isStopped = true;
        yield return new WaitForSeconds(seconds);
        _agent.isStopped = false;
    }
}
