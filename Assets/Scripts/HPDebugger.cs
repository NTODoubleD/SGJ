using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Slider))]
public class HPDebugger : MonoBehaviour
{
    [SerializeField]private DamageSystem damageSystem;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = damageSystem._maxHealth;
    }

    private void Update()
    {
        slider.value = damageSystem._health;
    }
}
