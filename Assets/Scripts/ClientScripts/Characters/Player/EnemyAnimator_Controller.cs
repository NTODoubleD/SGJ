using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator_Controller : All_AnimatorController
{
    public MeleeWeapon meleeWeapon;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _weapon = meleeWeapon;
        _weapon.onAttack.AddListener(Attack);
        Instance = this;
        isAttack = false;
        canDamage = false;
        onChangeAttack.AddListener(SetAttack);
        OnChangeDamage.AddListener(SetDamage);
    }

    private void SetDamage()
    {
        meleeWeapon.SetCanDamage(canDamage);
    }

    private void SetAttack()
    {
        meleeWeapon.SetIsAttacking(isAttack);
    }
}
