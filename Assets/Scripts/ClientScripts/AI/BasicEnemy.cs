using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BasicEnemy : Enemy
{
    [SerializeField] private float _stoppingDistance = 1.5f;

    protected bool _canAttack = true;

    protected override void Awake()
    {
        base.Awake();
        _weapon = GetComponentInChildren<Weapon>();
        
        _weapon.SetIsAttacking(true);
    }

    protected virtual void Start()
    {
        _state = EnemyStates.idle;
        _weapon.SetCanDamage(true);
        ImperialClass.Instance.OnStateChange += ChangeState;
        
        _agent.stoppingDistance = _stoppingDistance;
        _agent.updateRotation = false;
    }


    protected virtual void ChangeState()
    {
        if (_isDead is false)
        {
            switch (ImperialClass.Instance.State)
            {
                case ImperialStates.HuntingPlayer:
                    _state = EnemyStates.attackPlayer;
                    _agent?.SetDestination(PlayerBehaviour.Instance.Position);
                    break;
                default:
                    _state = EnemyStates.idle;
                    break;
            }
        }
    }

    protected virtual void Update()
    {
        if (_state == EnemyStates.attackPlayer)
        {
            TryAttackPlayer();
        }
    }

    protected virtual void TryAttackPlayer()
    {
        
        RotateTowards(PlayerBehaviour.Instance.Position);

        Vector3 vector = PlayerBehaviour.Instance.Position - transform.position;
        float VectorLenght =  Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));

        if (VectorLenght - 1 <= _stoppingDistance)
            AttackPlayer();

        if (VectorLenght > _stoppingDistance)
            FollowPlayer(); 

    }

    protected virtual void FollowPlayer()
    {
        _agent.SetDestination(PlayerBehaviour.Instance.Position);
    }

    protected virtual void AttackPlayer()
    {
        if (_canAttack is false)
            return;

        _weapon.TryAttack();
        StartCoroutine(ReloadAttack());
        print("attacking");


    }

    protected void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
    }

    protected IEnumerator ReloadAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(1.5f);
        _canAttack = true;
    }



}
