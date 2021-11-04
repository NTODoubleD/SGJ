using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsWindowsSwitcher : MonoBehaviour
{
    public Transform notUseGm, useGm;
    public GameObject[] panels;
    public GameObject[] settingsElements, graphicsElements, audioElements, controlsElements;

    private void Start()
    {
        ShowSettings();
    }

    public void ShowSettings()
    {
        Switch(0);
    }
    public void ShowGraphics()
    {
        Switch(1);
    }
    public void ShowAudio()
    {
        Switch(2);
    }
    public void ShowControls()
    {
        Switch(3);
    }
    public void Switch(int index)
    {
        ShowPanel(index);
        switch (index)
        {
            case 0:
                Show(settingsElements);
                Hide(graphicsElements);
                Hide(audioElements);
                Hide(controlsElements);
                break;
            case 1:
                Hide(settingsElements);
                Show(graphicsElements);
                Hide(audioElements);
                Hide(controlsElements);
                break;
            case 2:
                Hide(settingsElements);
                Hide(graphicsElements);
                Show(audioElements);
                Hide(controlsElements);
                break;
            case 3:
                Hide(settingsElements);
                Hide(graphicsElements);
                Hide(audioElements);
                Show(controlsElements);
                break;
        }
    }

    private void ShowPanel(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == index)
                panels[i].SetActive(true);
            else
                panels[i].SetActive(false);
        }
    }

    private void Show(GameObject[] gms)
    {
        foreach (var item in gms)
        {
            int childC = item.transform.childCount;
            for (int i = 0; i < childC; i++)
            {
                item.transform.GetChild(i).gameObject.SetActive(true);
            }
            item.transform.SetParent(useGm);
        }
    }
    private void Hide(GameObject[] gms)
    {
        foreach (var item in gms)
        {
            int childC = item.transform.childCount;
            for (int i = 0; i < childC; i++)
            {
                item.transform.GetChild(i).gameObject.SetActive(false);
            }
            item.transform.SetParent(notUseGm);
        }

    }
}
