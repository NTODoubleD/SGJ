using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ActionOnTriggerEnter : MonoBehaviour
{

    public UnityEvent _event;
    private void OnTriggerEnter(Collider other)
    {
        _event?.Invoke();
    }
}
