using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopnikEnemy : BasicEnemy
{
    [SerializeField] private GopnikAnimator _gopnikAnimator;

    private bool _isAttacking = false;

    protected override void ChangeState()
    {
        if (ImperialClass.Instance.State == ImperialStates.HuntingPlayer)
        {
            StartCoroutine(SetKnife());
            if (_isAttacking)
                return;
            _isAttacking = true;
            _gopnikAnimator.PrepareHuntAnimation();
            StartCoroutine(ChangeStateCoroutine());
        }


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
        _gopnikAnimator?.GetDamage();
    }


    private IEnumerator SetKnife()
    {
        yield return new WaitForSeconds(0.4f);
        _weapon?.ChangeExsist(true);
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
                    break;
                default:
                    _state = EnemyStates.idle;
                    break;
            }
        }
    }
}
