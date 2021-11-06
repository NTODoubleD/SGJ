using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class MeleeWeapon : Weapon
{
    private Collider _collider;
    [HideInInspector] public DamageSystem Instance;

    protected float _attackRadius; // Where it hits

    [SerializeField] private float _knockbackAmount = 1;
    private bool isEnter, isSmash;


    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        isEnter = true;
        isSmash = false;
        GiveDamage(other);
    }

    private void GiveDamage(Collider other)
    {
        if (_canDamage)
        {
            DamageSystem otherDamageSystem;
            if (other.gameObject.TryGetComponent<DamageSystem>(out otherDamageSystem))
            {
                isSmash = true;
                otherDamageSystem?.GetDamage(_damage);
                otherDamageSystem?.SetKnockback(transform.parent.position, _knockbackAmount);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isEnter && !isSmash)
        {
            GiveDamage(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isEnter = false;
    }


    private void FixedUpdate()
    {
        InputCheck();


        if (_isAttacking is false)
            return;

       // transform.RotateAround(transform.parent.position, Vector3.up, 90 * Time.fixedDeltaTime * _reloadTime);
    }


    protected override void Attack()
    {
        onAttack.Invoke();
    }


    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryAttack();
    }
}
