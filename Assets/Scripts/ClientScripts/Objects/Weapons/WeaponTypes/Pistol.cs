using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : FirearmWeapon
{
    [SerializeField] private GameObject _model;
    [SerializeField] private WeaponAudioClip _attackClip, _getClip, _reloadClip;


    protected override void Awake()
    {
        base.Awake();
        OnExsistChange += SetModel;
    }

    protected override void Start()
    {
        base.Start();
        onAttack.AddListener(PlayAttack);
        _attackClip.SetAudio();
        _getClip.SetAudio();
        _reloadClip.SetAudio();
    }

    protected void PlayAttack()
    {
        _attackClip.PlayAudio();
        StartCoroutine(PlayReloadAudio());
    }

    public override void ChangeExsist(bool exsist)
    {
        base.ChangeExsist(exsist);
        if (exsist)
        {
            _getClip.PlayAudio();
        }
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
    private IEnumerator PlayReloadAudio()
    {
        yield return new WaitForSeconds(_reloadTime / 2);
        _reloadClip.PlayAudio();
    }





}
