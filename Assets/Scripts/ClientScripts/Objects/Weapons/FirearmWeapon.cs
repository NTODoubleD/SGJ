﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmWeapon : Weapon
{
    [SerializeField] private float _aimValue = 40f;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _muzzlePosition;
    [SerializeField] private GameObject _particle;
    protected float _startAimValue;
    private float _startMouseSensivity;

    protected override void Start()
    {
        base.Start();
        _startAimValue = PlayerBehaviour.Instance.PlayerCamera.fieldOfView;
    }

    protected void Update()
    {
        InputCheck();
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryAttack();
        if (Input.GetKeyDown(KeyCode.Mouse1))
            Aim(true);
        if (Input.GetKeyUp(KeyCode.Mouse1))
            Aim(false);

    }

    protected override void Attack()
    {
        FireBullet();
        onAttack.Invoke();
        Reload();
    }

    protected void Aim (bool isActive)
    {
        if (isActive)
        {
            PlayerBehaviour.Instance.PlayerCamera.fieldOfView = _aimValue;
        }
        else
        {
            PlayerBehaviour.Instance.PlayerCamera.fieldOfView = _startAimValue;
        }

    }

    private void FireBullet()
    {
        Instantiate(_bullet, _muzzlePosition.position, _muzzlePosition.rotation);
        var newParticle = Instantiate(_particle, _muzzlePosition.position, _muzzlePosition.rotation);
        newParticle.transform.parent = transform;
    }
}