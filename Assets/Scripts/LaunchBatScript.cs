using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LaunchBatScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Process process = new Process();
        print(Application.dataPath);
        process.StartInfo.FileName = Application.dataPath + "/Scripts/ScriptBat/testLauncher.bat";
        process.Start();
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
