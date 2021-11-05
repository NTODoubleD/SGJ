using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : Enemy
{
    private void Start()
    {
        _state = EnemyStates.attackPlayer;
    }

    private void Update()
    {
        if (_state == EnemyStates.attackPlayer)
        {
            TryAttackPlayer();
        }
    }

    private void TryAttackPlayer()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        _agent.SetDestination(PlayerBehaviour.Instance.Position);
    }




}
