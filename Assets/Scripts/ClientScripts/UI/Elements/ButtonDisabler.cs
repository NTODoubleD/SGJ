using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ButtonDisabler : MonoBehaviour
{
    private Text text;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = transform.GetComponentInParent<Button>();
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (button.interactable && text.color != Color.white)
            text.color = Color.white;
        else if(text.color != Color.grey && !button.interactable)
            text.color = Color.grey;
    }


}
