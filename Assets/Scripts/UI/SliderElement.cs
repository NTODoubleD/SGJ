using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderElement : MonoBehaviour
{
    public Slider slider;
    public Text text;
    private float value;

    public float GetValue()
    {
        return value;
    }

    public void SetValue(float value)
    {
        this.value = value;
        SetVisual();
    }


    private void Awake()
    {
        slider.onValueChanged.AddListener(ChangeValue);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangeValue);
    }

    private void ChangeValue(float newValue)
    {
        value = newValue;
        SetVisual();
    }

    private void SetVisual()
    {
        slider.value = value;
        text.text = value.ToString();
    }
}
