using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UnityEngine;
using SDG.Unturned;

namespace HookSystem
{
    public class Vars
    {
        public static string managed = Directory.GetCurrentDirectory() + @"\Unturned_Data\Managed\";
        public static string save = Application.persistentDataPath + @"\";
        public static bool running = false;
        public static string url = "";
        public static string version = "";
        public static bool pMode = false;
        public static string title = "";
        public static string name = "";
        public static string creator = "";
        public static GameObject systemObject = null;
        public static Control controller = null;
        public static Assembly asm = null;
        public static bool onDisk = true;
    }
}
