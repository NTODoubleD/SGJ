using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleElement : MonoBehaviour
{
    public Button button;
    public Text text;
    private bool value;

    public bool GetValue()
    {
        return value;
    }

    public void SetValue(bool value)
    {
        this.value = value;
        SetVisual();
    }


    private void Awake()
    {
        button.onClick.AddListener(ChangeValue);
    }

    private void ChangeValue()
    {
        value = !value;
        SetVisual();
    }

    private void SetVisual()
    {
        if (value)
            text.text = "ON";
        else
            text.text = "OFF";
    }
}
