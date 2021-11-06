using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopnikKnife : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _mediator;

    private void Awake()
    {
        _weapon.ChangeExsist(false);
        SetExsist(false);
    }
    private void Start()
    {
        _weapon.OnExsistChange += SetExsist;
    }

    private void OnDestroy()
    {
        _weapon.OnExsistChange -= SetExsist;
    }

    private void SetExsist(bool exsist)
    {
        _mediator.SetActive(exsist);
    }
}
