using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopnikEnemy : BasicEnemy
{
    [SerializeField] private GopnikAnimator _gopnikAnimator;
    


    protected override void ChangeState()
    {
        _gopnikAnimator.PrepareHuntAnimation();
        StartCoroutine(ChangeStateCoroutine());
    }

    protected override void AttackPlayer()
    {
        if (_canAttack is false)
            return;

        _weapon.TryAttack();
        StartCoroutine(ReloadAttack());
        _gopnikAnimator.Attack();
    }

    public override void Damage()
    {
        base.Damage();
        _gopnikAnimator.GetDamage();
    }

    private IEnumerator ChangeStateCoroutine()
    {
        yield return new WaitForSeconds(1f);
        if (_isDead is false)
        {
            switch (ImperialClass.Instance.State)
            {
                case ImperialStates.HuntingPlayer:
                    _state = EnemyStates.attackPlayer;
                    _agent?.SetDestination(PlayerBehaviour.Instance.Position);
                    _gopnikAnimator.SetHuntAnimation();
                    break;
                default:
                    _state = EnemyStates.idle;
                    break;
            }
        }
    }
}
