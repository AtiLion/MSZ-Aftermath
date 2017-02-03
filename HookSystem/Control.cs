using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace HookSystem
{
    public class Control : MonoBehaviour
    {
        public void Start()
        {
            if (Vars.onDisk)
            {
                Funcs.fakeData();
                Vars.asm = Assembly.Load(File.ReadAllBytes(Directory.GetCurrentDirectory() + @"\Unturned_Data\Managed\MSZ Aftermath.dll"));
                if (!Vars.pMode && Vars.version != Provider.APP_VERSION)
                {
                    Debug.LogError("ERROR: Invalid version!");
                    return;
                }
                Debug.Log("Running hack...");
                Funcs.runHack();
            }
            else
            {
                Debug.Log("Collecting data...");
                if (Funcs.getData())
                {
                    Debug.Log("Data collected!");
                    if (Funcs.loadAssembly())
                    {
                        if (!Vars.pMode && Vars.version != Provider.APP_VERSION)
                        {
                            Debug.LogError("ERROR: Invalid version!");
                            return;
                        }
                        Debug.Log("Running hack...");
                        Funcs.runHack();
                    }
                }
                else
                {
                    Debug.LogError("ERROR! Cannot run the hack!");
                }
            }
        }
    }
}
