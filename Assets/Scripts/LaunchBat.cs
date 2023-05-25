using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class LaunchBat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Process process = new Process();
        process.StartInfo.FileName = Application.dataPath + "/Scripts/ScriptBat/LauncherAfterGameBroken.bat";
        process.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
