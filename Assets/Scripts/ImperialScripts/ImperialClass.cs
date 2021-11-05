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
    Dialogue
}

public class ImperialClass
{
    public static ImperialClass Instance;

    private ImperialStates _state;
    public ImperialStates State => _state;

    public Action OnStateChange;

    public ImperialClass()
    {
        Instance = this;
    }

    public void SetState(ImperialStates newState)
    {
        _state = newState;
        HandleState();
    }

    private void HandleState()
    {
        OnStateChange?.Invoke();
    }





}
