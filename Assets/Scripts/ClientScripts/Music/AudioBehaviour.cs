using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : MonoBehaviour
{
    public AudioBehaviour Instance;

    private void Awake()
    {
        Instance = this;
    }
}
