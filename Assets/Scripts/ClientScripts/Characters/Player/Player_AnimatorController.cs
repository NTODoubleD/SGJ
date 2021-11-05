using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player_AnimatorController : MonoBehaviour
{
    private Animator _animator;
    private Weapon _weapon;
    private PlayerMover _playerMover;
    private bool canDamage;
    private bool firstAttack;
    private bool isAttack;
    private void Start()
    {
        _playerMover = PlayerBehaviour.Instance.playerMover;
        _animator = GetComponent<Animator>();
        _playerMover.OnMove.AddListener(SetValues);
        _weapon = PlayerBehaviour.Instance.weapon;
        _weapon.onAttack.AddListener(Attack);
    }

    private void Attack()
    {
        if (firstAttack)
            _animator.SetTrigger("Slash");
        else
            _animator.SetTrigger("Slash1");
        firstAttack = !firstAttack;
    }
    private void SetValues()
    {
        _animator.SetFloat("SpeedOfMoving", _playerMover.velocity);
    }
    
    public void StartAttack()
    {
        isAttack = true;
        _weapon.SetIsAttacking(true);
    }

    public void EndAttack()
    {
        isAttack = false;
        _weapon.SetIsAttacking(false);
    }

    public void StartDamage()
    {
        canDamage = true;
        _weapon.SetCanDamage(true);
        print("Start");
    }

    public void EndDamage()
    {
        canDamage = false;
        _weapon.SetCanDamage(false);
        print("End");
    }
}
