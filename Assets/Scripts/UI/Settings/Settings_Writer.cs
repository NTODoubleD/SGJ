using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Settings_Writer : MonoBehaviour
{
    private StreamWriter writer;
    private string path;

    private void ConfigureWriter()
    {
        path = Application.dataPath + "/StreamingAssets";
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        path += "/config.txt";
        writer = new StreamWriter(path, false, System.Text.Encoding.Default);
    }

    public void StartSave(/*Setting_Data data*/)
    {
        ConfigureWriter();
        WriteLines(/*data*/);
        writer.Close();
    }
    private void WriteLines(/*Setting_Data data*/)
    {
        writer.WriteLine(Setting_Data.resoulution_index);
        writer.WriteLine(Setting_Data.isFullScreen);
        writer.WriteLine(Setting_Data.texQuality_index);
        //writer.WriteLine(Setting_Data.antiAliasingMSAA_Index);
        writer.WriteLine(Setting_Data.mixerVolume_Index);
        writer.WriteLine(Setting_Data.mixerMusic_Index);
        writer.WriteLine(Setting_Data.mixerSound_Index);
        writer.WriteLine(Setting_Data.sensitivity_Index);
        writer.WriteLine(Setting_Data.vSync_Index);
        writer.WriteLine(Setting_Data.isReflectionProbes);
    }
}
