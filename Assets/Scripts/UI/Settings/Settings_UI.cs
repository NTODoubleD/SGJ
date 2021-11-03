using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Settings_UI : MonoBehaviour
{
    public SelectItemBox resoulutionSelectBox, TextureQualitySelectBox, AntiAliasingMSAASelectBox;
    public ToggleElement fullScreenToggle;
    [HideInInspector]public UnityEvent OnApply;
    public Resolution[] resolutions;

    public void StartConfig()
    {
        resolutions = Screen.resolutions;
        resoulutionSelectBox.SetTexts(FromResToStrings(resolutions));
    }

    public void SetStartSettings()
    {
        Setting_Data.resoulution_index = Setting_Data.FindIndexCurResoulution();
        Setting_Data.isFullScreen = Screen.fullScreen;
        Setting_Data.texQuality_index = 3 + QualitySettings.masterTextureLimit;
        Setting_Data.antiAliasingMSAA_Index = QualitySettings.antiAliasing;

        SetLoadedSettings();
        OnApply.Invoke();
    }

    public void SetLoadedSettings()
    {
        resoulutionSelectBox.SetValue(Setting_Data.resoulution_index);
        fullScreenToggle.SetValue(Setting_Data.isFullScreen);
        TextureQualitySelectBox.SetValue(Setting_Data.texQuality_index);
        AntiAliasingMSAASelectBox.SetValue(Setting_Data.antiAliasingMSAA_Index);

        OnApply.Invoke();
    }

    public void GetCurrentData()
    {
        Setting_Data.isFullScreen = fullScreenToggle.GetValue();
        Setting_Data.resoulution_index = resoulutionSelectBox.GetValue();
        Setting_Data.texQuality_index = TextureQualitySelectBox.GetValue();
        Setting_Data.antiAliasingMSAA_Index = AntiAliasingMSAASelectBox.GetValue();
    }

    private string[] FromResToStrings(Resolution[] resolutions)
    {
        string[] array = new string[resolutions.Length];
        for (int i = 0; i < resolutions.Length; i++)
        {
            array[i] = resolutions[i].ToString();

            //string[] words = array[i].Split(new char[] { '@' });
            //array[i] = words[0];
        }
        return array;
    }
}
