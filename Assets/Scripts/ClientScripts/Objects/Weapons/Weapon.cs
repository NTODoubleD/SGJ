using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponParameters _parameters;
    protected bool _canAttack = true;
    protected float _reloadTime;
    protected int _damage;

    protected virtual void Start()
    {
        _reloadTime = _parameters.ReloadTime;
        _damage = _parameters.Damage;
    }


    public virtual void TryAttack()
    {
        if (_canAttack)
            Attack();
    }

    protected virtual void Attack()
    {
        Reload();
    }

    protected virtual void Reload()
    {
        StartCoroutine(BasicReload());
    }

    protected IEnumerator BasicReload()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_reloadTime);
        _canAttack = true;

    }
}
