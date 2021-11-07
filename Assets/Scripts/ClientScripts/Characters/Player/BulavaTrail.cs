using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulavaTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;


    private void Start()
    {
        All_AnimatorController.Instance.OnAnimationChange += ChangeTrail;
    }

    private void OnDisable()
    {
        All_AnimatorController.Instance.OnAnimationChange -= ChangeTrail;
    }

    private void ChangeTrail(bool active)
    {
        _trail.emitting = active;
    }




}
