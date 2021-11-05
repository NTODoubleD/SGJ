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

    protected bool _canAttack = true;

    [HideInInspector] public UnityEvent onAttack;

    protected void SetImperialState()
    {
        switch (ImperialClass.Instance.State)
        {
            case ImperialStates.Dialogue:
                _canAttack = false;
                break;
            default:
                _canAttack = true;
                break;
        }
    }
    public void SetIsAttacking(bool isAttack)
    {
        _isAttacking = isAttack;
    }
    public void SetCanDamage(bool canDamage)
    {
        _canDamage = canDamage;
    }

    protected virtual void Awake()
    {
        _canDamage = false;
    }

    protected virtual void Start()
    {
        _reloadTime = _parameters.ReloadTime;
        _damage = _parameters.Damage;
        _isAttacking = false;
        ImperialClass.Instance.OnStateChange += SetImperialState;
}


    public virtual void TryAttack()
    {
        if (!_isAttacking && _canAttack)
            Attack();
    }

    protected virtual void Attack()
    {
        print("attack");
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
