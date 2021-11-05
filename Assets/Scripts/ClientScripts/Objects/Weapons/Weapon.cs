using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponParameters _parameters;
    protected bool _canDamage = true;
    protected bool _isAttacking = false;
    protected float _reloadTime;
    protected int _damage;
    [HideInInspector]public UnityEvent onAttack;
    public void SetIsAttacking(bool isAttack)
    {
        _isAttacking = isAttack;
    }
    public void SetCanDamage(bool canDamage)
    {
        _canDamage = canDamage;
    }

    protected virtual void Start()
    {
        _reloadTime = _parameters.ReloadTime;
        _damage = _parameters.Damage;
    }


    public virtual void TryAttack()
    {
        if (!_isAttacking)
            Attack();
    }

    protected virtual void Attack()
    {
        onAttack.Invoke();
        Reload();
    }

    protected virtual void Reload()
    {
        StartCoroutine(BasicReload());
    }

    protected IEnumerator BasicReload()
    {
        _canDamage = false;
        yield return new WaitForSeconds(_reloadTime);
        _canDamage = true;

    }
}
