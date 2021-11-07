using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


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
    [SerializeField] protected int _team;
    protected Rigidbody _rigidbody;

    protected bool _isDead = false;

    protected static Dictionary<int, int> _teams = new Dictionary<int, int>();
    public static Action<int> OnTeamDead;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        SetTeam();
    }

    private void SetTeam()
    {
        foreach(var item in _teams)
        {
            if (item.Key == _team)
            {
                _teams[item.Key]++;
                return;
            }
        }

        _teams.Add(_team, 1);
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

    public void Die()
    {
        _isDead = true;
        _teams[_team]--;
        if (_teams[_team] <= 0)
            OnTeamDead?.Invoke(_team);
    }

    public virtual void Damage()
    {

    }

    public void TryStartFight()
    {
        ImperialClass.Instance.SetHuntPlayer(_team);

    }
}
