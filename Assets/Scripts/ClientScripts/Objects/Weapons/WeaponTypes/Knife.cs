using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Sword
{
    [SerializeField] private WeaponAudioClip _attackClip, _getClip, _hit;

    protected override void Start()
    {
        base.Start();
        onAttack.AddListener(PlayAttack);
        _attackClip.SetAudio();
        _getClip.SetAudio();
        _hit.SetAudio();
    }

    protected void PlayAttack()
    {
        if (_exsist)
            _attackClip.PlayAudio();
    }

    protected override void GiveDamage(Collider other)
    {
        base.GiveDamage(other);
        if (_exsist)
            _hit.PlayAudio(0.5f, true);
    }

    public override void ChangeExsist(bool exsist)
    {
        base.ChangeExsist(exsist);
        if (exsist)
        {
            _getClip.PlayAudio();
        }
    }
}
