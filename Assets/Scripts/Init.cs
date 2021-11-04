using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Basically this script lauch everything

public class Init : MonoBehaviour
{
    private void Awake()
    {
        new ImperialClass();
    }

    private void Update() // Test, delete later
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ImperialClass.Instance.SetState(ImperialStates.Idle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ImperialClass.Instance.SetState(ImperialStates.PlayerMove);
        }
    }
}
