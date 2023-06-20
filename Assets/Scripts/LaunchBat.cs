using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class LaunchBat : MonoBehaviour
{

    public void ExitAppThenRestart()
    {
        Process process = new Process();
        process.StartInfo.FileName = Application.dataPath + "/LauncherAfterGameBroken.bat";
        print(Directory.GetParent(Application.dataPath));
        process.Start();

        Application.Quit();
    }

  
}
