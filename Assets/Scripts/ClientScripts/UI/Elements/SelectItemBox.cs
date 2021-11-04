using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectItemBox : MonoBehaviour
{
    public Button next, previous;
    public Text text;

    private int value;

    public string[] texts;

    public UnityEvent OnValueChange;

    public int GetValue()
    {
        return value;
    }

    public void SetValue(int value)
    {
        if (value >= 0 && value <= texts.Length - 1)
        {
            this.value = value;
            SetButtonsActive();
            SetText();
        }
        else Debug.LogError("SetValue Error");
    }
    public void SetTexts(string[] array)
    {
        texts = array;
    }

    private void Start()
    {
        SetButtonsActive();
        SetText();

        next.onClick.AddListener(Next);
        previous.onClick.AddListener(Previous);

        OnValueChange.AddListener(SetText);
    }

    private void Next()
    {
        if (value < texts.Length - 1)
        {
            value++;
            SetButtonsActive(); 
            OnValueChange.Invoke();
        }
        
    }

    private void Previous()
    {
        if (value > 0)
        {
            value--;
            SetButtonsActive();
            OnValueChange.Invoke();
        }
    }

    private void SetButtonsActive()
    {
        next.gameObject.SetActive(value < texts.Length - 1);
        previous.gameObject.SetActive(value > 0);
    }

    private void SetText()
    {
        text.text = texts[value];
    }
}
