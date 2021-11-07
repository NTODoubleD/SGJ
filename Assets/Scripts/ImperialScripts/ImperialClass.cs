using UnityEngine;
using System;


// Class that change everything in the game:
// Player controls, UI, music and etc.
// All the project based on singletone 
// Wish me luck XD


public enum ImperialStates
{
    Idle, // Nothing happens, all is disabled (expect UI or something, idk)
    PlayerMove,
    Dialogue,
    HuntingPlayer
}

public class ImperialClass : MonoBehaviour
{
    public static ImperialClass Instance;

    [SerializeField] private ImperialStates _state;
    public ImperialStates State => _state;
    private int _huntingPlayerTeam;

    public int HuntingTeam => _huntingPlayerTeam;
    public Action<int> OnHuntingPlayer;

    public Action OnStateChange;

    private void Awake()
    {
        Instance = this;
        Enemy.OnTeamDead += SetMoveState;
    }


    private void SetMoveState(int team)
    {
        SetState(ImperialStates.PlayerMove);
    }

    public void SetState(ImperialStates newState, bool forceState = false)
    {
        if (_state != newState || forceState)
        {
            _state = newState;
            HandleState();
        }

    }

    public void SetHuntPlayer(int team)
    {
        if (_huntingPlayerTeam == team)
            return;
        _huntingPlayerTeam = team;
        SetState(ImperialStates.HuntingPlayer, true);
        OnHuntingPlayer?.Invoke(_huntingPlayerTeam);
        
    }

    private void HandleState()
    {
        OnStateChange?.Invoke();
    }





}
