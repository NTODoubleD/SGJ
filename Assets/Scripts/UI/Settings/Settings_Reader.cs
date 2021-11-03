using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Settings_Reader : MonoBehaviour
{
    private StreamReader reader;
    private string path;
    [HideInInspector] public UnityEvent onNotExist, onExist;

    private void Configure()
    {
        path = Application.dataPath + "/StreamingAssets";
        path += "/config.txt";
    }

    public void GetData()
    {
        Configure();
        if (!File.Exists(path))
        {
            Debug.LogError("Файл подкачки отсутвует");
            onNotExist.Invoke();
        }
        else
        {
            reader = new StreamReader(path);
            ComposeData();
            CloseReader();
            onExist.Invoke();
        }
    }

    private void ComposeData()
    {
        //Setting_Data data = new Setting_Data();

        int.TryParse(reader.ReadLine(), out Setting_Data.resoulution_index);
        bool.TryParse(reader.ReadLine(), out Setting_Data.isFullScreen);
        int.TryParse(reader.ReadLine(), out Setting_Data.texQuality_index);
        int.TryParse(reader.ReadLine(), out Setting_Data.antiAliasingMSAA_Index);
        float.TryParse(reader.ReadLine(), out Setting_Data.mixerVolume_Index);
        float.TryParse(reader.ReadLine(), out Setting_Data.sensitivity_Index);
        int.TryParse(reader.ReadLine(), out Setting_Data.vSync_Index);
        bool.TryParse(reader.ReadLine(), out Setting_Data.isReflectionProbes);

    }

    private void CloseReader()
    {
        reader.Close();
    }
}
