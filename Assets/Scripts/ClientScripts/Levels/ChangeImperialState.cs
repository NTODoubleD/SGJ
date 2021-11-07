using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImperialState : MonoBehaviour
{
    public void ChangeState(ImperialStates state)
    {
        ImperialClass.Instance.SetState(state);
    }

    public void SetPlayerMove()
    {
        ImperialClass.Instance.SetState(ImperialStates.PlayerMove);
    }
}
