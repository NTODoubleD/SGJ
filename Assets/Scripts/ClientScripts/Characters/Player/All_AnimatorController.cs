using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class All_AnimatorController : MonoBehaviour
{
    [HideInInspector] public Animator _animator;
    [HideInInspector] public Weapon _weapon;
    public bool isPlayer;
    private bool firstAttack;
    [HideInInspector] public bool canDamage;
    [HideInInspector] public bool isAttack;

    public static All_AnimatorController Instance;
    public Action<bool> OnAnimationChange;

    [HideInInspector] public UnityEvent onChangeAttack, OnChangeDamage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _weapon = PlayerBehaviour.Instance.weapon;
        _weapon.onAttack.AddListener(Attack);
        isAttack = false;
        canDamage = false;
    }

    public void Attack()
    {
        if (isPlayer)
        {
            if (firstAttack)
                _animator.SetTrigger("Slash");
            else
                _animator.SetTrigger("Slash1");
            firstAttack = !firstAttack;
        }
    }

    
    public void StartAttack()
    {
        isAttack = true;
        OnAnimationChange?.Invoke(isAttack);
        _weapon.SetIsAttacking(true);
        onChangeAttack.Invoke();
    }

    public void EndAttack()
    {
        isAttack = false;
        OnAnimationChange?.Invoke(isAttack);
        _weapon.SetIsAttacking(false);
        onChangeAttack.Invoke();
    }

    public void StartDamage()
    {
        canDamage = true;
        _weapon.SetCanDamage(true);
        //print("Start");
        OnChangeDamage.Invoke();
    }

    public void EndDamage()
    {
        canDamage = false;
        _weapon.SetCanDamage(false);
        OnChangeDamage.Invoke();
        //print("End");
    }
}
