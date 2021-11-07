using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLight : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _speed;

    public void Update()
    {
        _light.intensity -= _speed * Time.deltaTime;
    }
}
