using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DamageSystem))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerBehaviour : MonoBehaviour
{
    [HideInInspector]public PlayerMover playerMover;
    [SerializeField] public Weapon weapon;
    public static PlayerBehaviour Instance;
    private DamageSystem _damageSystem;


    public Vector3 Position => transform.position;

    private bool _canMove = false;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMover>();
        _damageSystem = GetComponent<DamageSystem>();
        _damageSystem.DestroyOnDead = false;
        Instance = this;

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
            case ImperialStates.Idle:
                _canMove = false;
                break;
        }

        SetMovable();
    }


    private void SetMovable()
    {
        playerMover.CanMove = _canMove;
    }

}
