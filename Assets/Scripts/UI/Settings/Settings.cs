using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Settings_Writer), typeof(Settings_Reader))]
public class Settings : MonoBehaviour
{
    public Settings_UI settings_ui;
    
    private Settings_Writer writer;
    private Settings_Reader reader;

    public static float Sensitivity;

    //public HDRenderPipelineAsset hdRenderPipeline;

    public Resolution[] resolutions;

    void Awake()
    {
        GetComponents();
        StartConfig();
        ReadSettings_Data();
    }

    private void GetComponents()
    {
        writer = GetComponent<Settings_Writer>();
        reader = GetComponent<Settings_Reader>();
    }

    private void StartConfig()
    {
        resolutions = Screen.resolutions;

        settings_ui.StartConfig();
        settings_ui.OnApply.AddListener(Apply);

        reader.onNotExist.AddListener(settings_ui.SetStartSettings);
        reader.onExist.AddListener(settings_ui.SetLoadedSettings);
    }

    private void ReadSettings_Data()
    {
        reader.GetData();
    }

    private void Save()
    {
        settings_ui.GetCurrentData();
        writer.StartSave();
    }

    public void Apply()
    {
        settings_ui.GetCurrentData();

        int value = Setting_Data.resoulution_index;
        Screen.SetResolution(resolutions[value].width, resolutions[value].height, Setting_Data.isFullScreen);
        QualitySettings.masterTextureLimit = 3 - Setting_Data.texQuality_index;

        QualitySettings.antiAliasing = Setting_Data.antiAliasingMSAA_Index;
        settings_ui.audioMixer.SetFloat("Volume", Setting_Data.mixerVolume_Index);
        Sensitivity = Setting_Data.sensitivity_Index;
        QualitySettings.vSyncCount = Setting_Data.vSync_Index;
        QualitySettings.realtimeReflectionProbes = Setting_Data.isReflectionProbes;

        Save();
    }



}

public class Setting_Data
{
    public static int resoulution_index;
    public static bool isFullScreen;
    public static int texQuality_index;
    public static int antiAliasingMSAA_Index;
    public static float mixerVolume_Index;
    public static float sensitivity_Index;
    public static int vSync_Index;
    public static bool isReflectionProbes;

    public static int FindIndexCurResoulution()
    {
        Resolution[] resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.currentResolution.ToString() == resolutions[i].ToString())
            {
                return i;
            }
        }
        Debug.LogError("Resoulutions not Found");
        return 0;
    }
}

