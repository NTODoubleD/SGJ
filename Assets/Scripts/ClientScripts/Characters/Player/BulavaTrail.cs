using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulavaTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;
    private Gradient _normalGradiet;


    private void Start()
    {
        Player_AnimatorController.Instance.OnAnimationChange += ChangeTrail;
    }

    private void OnDisable()
    {
        Player_AnimatorController.Instance.OnAnimationChange -= ChangeTrail;
    }

    private void ChangeTrail(bool active)
    {
        _trail.emitting = active;
    }




}
