using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BasicEnemy : Enemy
{
    [SerializeField] private float _stoppingDistance = 1.5f;
    protected Weapon _weapon;

    protected override void Awake()
    {
        base.Awake();
        _weapon = GetComponentInChildren<Weapon>();
        
        _weapon.SetIsAttacking(true);
    }

    private void Start()
    {
        _state = EnemyStates.idle;
        _weapon.SetCanDamage(true);
        ImperialClass.Instance.OnStateChange += ChangeState;
        
        _agent.stoppingDistance = _stoppingDistance;
        _agent.updateRotation = false;
    }


    private void ChangeState()
    {
        switch (ImperialClass.Instance.State)
        {
            case ImperialStates.HuntingPlayer:
                _state = EnemyStates.attackPlayer;
                _agent.SetDestination(PlayerBehaviour.Instance.Position);
                break;
            default:
                _state = EnemyStates.idle;
                break;
        }


        
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
        FollowPlayer();
        RotateTowards(PlayerBehaviour.Instance.Position);
        if (_agent.remainingDistance <= _stoppingDistance)
            AttackPlayer();
            
    }

    private void FollowPlayer()
    {
        _agent.SetDestination(PlayerBehaviour.Instance.Position);
    }

    private void AttackPlayer()
    {
        
        _weapon.TryAttack();
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
    }




}
