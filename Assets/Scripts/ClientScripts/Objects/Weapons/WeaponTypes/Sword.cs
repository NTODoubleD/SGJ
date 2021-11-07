using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeWeapon
{
    [SerializeField] private MeshRenderer _renderer;

    protected override void Awake()
    {
        base.Awake();
        OnExsistChange += SetModel;
    }

    private void SetModel(bool exsist)
    {
        _renderer.enabled = exsist;
    }
}
