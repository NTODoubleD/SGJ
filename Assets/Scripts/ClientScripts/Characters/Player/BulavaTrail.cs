using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulavaTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private Weapon _weapon;


    private void Start()
    {
        All_AnimatorController.Instance.OnChangeDamage.AddListener(ChangeTrail);
        //_weapon.OnExsistChange += ChangeTrail;
    }

    private void OnDisable()
    {
        All_AnimatorController.Instance.OnChangeDamage.RemoveListener(ChangeTrail);
        //_weapon.OnExsistChange -= ChangeTrail;
    }

    private void ChangeTrail()
    {
        _trail.emitting = !_trail.emitting;
    }

    private void OffTrail(bool active)
    {
        if (active is false)
        {
            _trail.emitting = false;
        }
    }




}
