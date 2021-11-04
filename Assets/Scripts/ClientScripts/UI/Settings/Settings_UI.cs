using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class Settings_UI : MonoBehaviour
{
    public SelectItemBox resoulutionSelectBox, TextureQualitySelectBox/*, AntiAliasingMSAASelectBox*/, VSync;
    public ToggleElement fullScreenToggle, ReflectionProbes;
    public SliderElement mixerVolume, mixerMusic, mixerSound, mouseSensitivity;
    public AudioMixer audioMixer;

    [HideInInspector]public UnityEvent OnApply;
    public Resolution[] resolutions;

    public void StartConfig()
    {
        resolutions = Screen.resolutions;
        resoulutionSelectBox.SetTexts(FromResToStrings(resolutions));
    }

    public void SetStartSettings()
    {
        Setting_Data.resoulution_index = Screen.resolutions.Length - 1;
        Setting_Data.isFullScreen = true;
        Setting_Data.texQuality_index = 2;
        //Setting_Data.antiAliasingMSAA_Index = QualitySettings.antiAliasing;
        Setting_Data.mixerMusic_Index = 0;
        Setting_Data.mixerSound_Index = 0;
        Setting_Data.mixerVolume_Index = 0;
        Setting_Data.sensitivity_Index = 1;
        Setting_Data.vSync_Index = 2;
        Setting_Data.isReflectionProbes = true;


        SetLoadedSettings();
        OnApply.Invoke();
    }

    public void SetLoadedSettings()
    {
        resoulutionSelectBox.SetValue(Setting_Data.resoulution_index);
        fullScreenToggle.SetValue(Setting_Data.isFullScreen);
        TextureQualitySelectBox.SetValue(Setting_Data.texQuality_index);
        //AntiAliasingMSAASelectBox.SetValue(Setting_Data.antiAliasingMSAA_Index);
        mixerVolume.SetValue(Setting_Data.mixerVolume_Index != -80 ? (int)((Setting_Data.mixerVolume_Index + 20) * 2.5f) : 0);
        mixerMusic.SetValue(Setting_Data.mixerMusic_Index != -80 ? (int)((Setting_Data.mixerMusic_Index + 20) * 2.5f) : 0);
        mixerSound.SetValue(Setting_Data.mixerSound_Index != -80 ? (int)((Setting_Data.mixerSound_Index + 20) * 2.5f) : 0);
        mouseSensitivity.SetValue(Setting_Data.sensitivity_Index * 10);
        VSync.SetValue(Setting_Data.vSync_Index);
        ReflectionProbes.SetValue(Setting_Data.isReflectionProbes);


        OnApply.Invoke();
    }

    public void GetCurrentData()
    {
        Setting_Data.isFullScreen = fullScreenToggle.GetValue();
        Setting_Data.resoulution_index = resoulutionSelectBox.GetValue();
        Setting_Data.texQuality_index = TextureQualitySelectBox.GetValue();
        //Setting_Data.antiAliasingMSAA_Index = AntiAliasingMSAASelectBox.GetValue();
        Setting_Data.mixerVolume_Index = ((int)(mixerVolume.GetValue() / 2.5f) - 20 ) != -20 ? (int)(mixerVolume.GetValue() / 2.5f) - 20 : -80;
        Setting_Data.mixerMusic_Index = ((int)(mixerMusic.GetValue() / 2.5f) - 20) != -20 ? (int)(mixerMusic.GetValue() / 2.5f) - 20 : -80;
        Setting_Data.mixerSound_Index = ((int)(mixerSound.GetValue() / 2.5f) - 20) != -20 ? (int)(mixerSound.GetValue() / 2.5f) - 20 : -80;
        Setting_Data.sensitivity_Index = mouseSensitivity.GetValue() / 10;
        Setting_Data.vSync_Index = VSync.GetValue();
        Setting_Data.isReflectionProbes = ReflectionProbes.GetValue(); 
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
