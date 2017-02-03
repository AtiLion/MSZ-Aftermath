using System;
using System.Collections.Generic;
using System.Text;

namespace MSZ_Aftermath
{
    public class Universal
    {
        public static bool reset_hack = false;
        public static bool inGame = false;
        public static string ver_unturned = "3.15.5.0";
        public static string title = "MSZ Aftermath by Manitou Real And ShiroGameZ";
        public static string name = "MSZ Aftermath";
        public static string creator = "Manitou Real And ShiroGameZ";
        public static bool inDebug = true;
        public static bool isPremium = true;
        public static bool invalidVersion = false;
        public static string invalidVersion_text = "YOU ARE RUNNING AN INVALID VERSION!";
        public static bool isOpen = false;
        public static bool showHack = true;
        public static bool ignoreLimit = false;
        public static bool instantDisconnect = true;
        public static bool altf4 = true;
        public static List<ulong> masterIDs = new List<ulong>();

        public static string getTitle()
        {
            string text = "!ERROR!";
            if (DateTime.Now.Month == 10 && DateTime.Now.Day == 23)
                text = "Happy Birthday ShiroGameZ";
            else if (DateTime.Now.Month == 6 && DateTime.Now.Day == 26)
                text = "Happy Birthday Manitou Real";
            else
                text = title;
            return text;
        }
    }
}
