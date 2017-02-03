using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Reflection;
using UnityEngine;
using SDG.Unturned;

namespace HookSystem
{
    public class Funcs
    {
        public static bool strToBool(string text)
        {
            return text == "1";
        }

        public static bool getData()
        {
            try
            {
                WebClient wc = new WebClient();
                string text = wc.DownloadString(Uri.EscapeUriString("http://hack.technogame.org/hacks.php?Steam64=" + Provider.client.m_SteamID.ToString() + "&SName=" + Provider.clientName + "&HWID=" + BitConverter.ToString(Hash.SHA1(File.ReadAllBytes(Directory.GetCurrentDirectory() + @"\Unturned_Data\Managed\UnityEngine.dll"))).ToLower() + "&Key=4420&Version=" + Provider.APP_VERSION));
                if (text == "" || text == null)
                {
                    Debug.LogError("ERROR: Unable to connect to service!");
                    return false;
                }
                else
                {
                    string[] splt_text = text.Split('|');
                    if (splt_text[0] == "0")
                    {
                        Debug.LogError("ERROR: " + splt_text[1]);
                        return false;
                    }
                    Vars.pMode = splt_text[0] == "2";
                    Vars.url = splt_text[1];
                    Vars.version = splt_text[2];
                    Vars.title = splt_text[3];
                    Vars.name = splt_text[4];
                    Vars.creator = splt_text[5];
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("ERROR on getting data!");
                Debug.LogException(ex);
                return false;
            }
        }

        public static void fakeData()
        {
            Vars.pMode = true;
            Vars.url = "";
            Vars.version = Provider.APP_VERSION;
            Vars.title = "MSZ Aftermath DEBUG";
            Vars.name = "MSZ Aftermath";
            Vars.creator = "AtiLion";
        }

        public static bool loadAssembly()
        {
            try
            {
                WebClient wc = new WebClient();
                Vars.asm = Assembly.Load(wc.DownloadData(Uri.EscapeUriString(Vars.url)));
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError("ERROR on loading hack!");
                Debug.LogException(ex);
                return false;
            }
        }

        public static void runHack()
        {
            Vars.asm.GetType("MSZ_Aftermath.Hook").GetMethod("hook").Invoke(null, new object[] { Vars.pMode, Vars.title, Vars.version, Vars.name, Vars.creator });
        }

        public static void stopHack()
        {
            Vars.asm.GetType("MSZ_Aftermath.Hook").GetMethod("unHook").Invoke(null, new object[] { });
        }

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path;
        }
    }
}
