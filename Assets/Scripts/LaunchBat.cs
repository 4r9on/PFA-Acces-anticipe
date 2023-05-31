using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class LaunchBat : MonoBehaviour
{

    public void ExitAppThenRestart()
    {
        Process process = new Process();
        process.StartInfo.FileName = Application.dataPath + "/Scripts/ScriptBat/LauncherAfterGameBroken.bat";
        process.Start();

        Application.Quit();
    }
}
