using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimatorController : All_AnimatorController
{
    private PlayerMover _playerMover;

    private void Start()
    {
        _playerMover = PlayerBehaviour.Instance.playerMover;
        _playerMover.OnMove.AddListener(SetValues); 
        _animator = GetComponent<Animator>();
        _weapon = PlayerBehaviour.Instance.weapon;
        _weapon.onAttack.AddListener(Attack);
        Instance = this; 
        isAttack = false;
        canDamage = false;
    }
    private void SetValues()
    {
        _animator.SetFloat("SpeedOfMoving", _playerMover.velocity);
    }
}
