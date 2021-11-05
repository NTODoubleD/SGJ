using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player_AnimatorController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private PlayerMover _playerMover;
    private bool canDamage;
    private bool firstAttack;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMover.OnMove.AddListener(SetValues);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(firstAttack )
                _animator.SetTrigger("Slash");
            else
                _animator.SetTrigger("Slash1");
            firstAttack = !firstAttack;
        }
    }

    private void SetValues()
    {
        _animator.SetFloat("SpeedOfMoving", _playerMover.velocity);
    }
    
    public void StartSlash()
    {
        canDamage = false;
        print("Start");
    }

    public void EndSlash()
    {
        canDamage = true;
        print("End");
    }
}
