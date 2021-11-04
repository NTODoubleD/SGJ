using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(PlayerMover))]
public class PlayerBehaviour : MonoBehaviour
{
    private PlayerMover _playerMover;

    private bool _canMove = false;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Start()
    {
        Debug.Log(ImperialClass.Instance);
        ImperialClass.Instance.OnStateChange += ChangeState;
        
    }

    private void OnDisable()
    {
        ImperialClass.Instance.OnStateChange -= ChangeState;
    }

    private void ChangeState()
    {
        switch (ImperialClass.Instance.State)
        {
            case ImperialStates.PlayerMove:
                _canMove = true;
                break;
            default:
                _canMove = false;
                break;
        }

        SetMovable();
    }


    private void SetMovable()
    {
        _playerMover.CanMove = _canMove;
    }




}
