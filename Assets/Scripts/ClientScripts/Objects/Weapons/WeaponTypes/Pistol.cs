using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : FirearmWeapon
{
    [SerializeField] private GameObject _model;

    protected override void Awake()
    {
        base.Awake();
        OnExsistChange += SetModel;
    }

    private void SetModel(bool exsist)
    {
        _model.SetActive(exsist);
    }

    public override void TryAttack()
    {
        if (!_isAttacking && _canAttack && _exsist && _isReloaded)
            Attack();
    }
}
