using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(DamageSystem))]
[RequireComponent(typeof(PlayerMover))]
public class PlayerBehaviour : MonoBehaviour
{
    [HideInInspector] public PlayerMover playerMover;
    [SerializeField] private Camera playerCamera;

    [SerializeField] private Weapon _meleeWeapon;
    [SerializeField] private Weapon _firearmWeapon;
    public Weapon weapon;

    public static PlayerBehaviour Instance;
    private DamageSystem _damageSystem;
    private LayerMask _raycastMask;

    public Camera PlayerCamera => playerCamera;

    public Action<RaycastHit> OnRaycast;


    public Vector3 Position => transform.position;

    private bool _canMove = false;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMover>();
        playerMover.PlayerCamera = playerCamera;
        _damageSystem = GetComponent<DamageSystem>();
        _damageSystem.DestroyOnDead = false;
        
        Instance = this;

    }

    private void Start()
    {
        Debug.Log(ImperialClass.Instance);
        ImperialClass.Instance.OnStateChange += ChangeState;

        _raycastMask = LayerMask.GetMask("Enemy");

        _meleeWeapon.ChangeExsist(true);
        _firearmWeapon.ChangeExsist(false);
    }

    private void OnDestroy()
    {
        ImperialClass.Instance.OnStateChange -= ChangeState;
    }

    private void Update()
    {
        Physics.Raycast(playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)), out RaycastHit hitInfo, 2f, _raycastMask.value);
        OnRaycast?.Invoke(hitInfo);

        InputCheck();
    }

    private void ChangeState()
    {
        switch (ImperialClass.Instance.State)
        {
            case ImperialStates.PlayerMove:
                _canMove = true;
                break;
            case ImperialStates.HuntingPlayer:
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
        playerMover.CanMove = _canMove;
    }

    private void InputCheck()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon(_meleeWeapon);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeapon(_firearmWeapon);
    }


    

    private void ChangeWeapon(Weapon newWeapon)
    {
        weapon.ChangeExsist(false);
        weapon = newWeapon;
        weapon.ChangeExsist(true);

    }

}
