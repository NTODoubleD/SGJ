using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class MeleeWeapon : Weapon
{
    private Collider _collider;

    protected float _attackRadius; // Where it hits

    [SerializeField] private float _knockbackAmount = 1;



    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_canDamage)
        {
            DamageSystem otherDamageSystem;
            if (other.gameObject.TryGetComponent<DamageSystem>(out otherDamageSystem))
            {
                otherDamageSystem.GetDamage(_damage);
                otherDamageSystem.SetKnockback(transform.parent.position, _knockbackAmount);
            }
        }
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
