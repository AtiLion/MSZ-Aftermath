using System;
using System.Collections.Generic;
using System.Text;
using SDG.Unturned;
using UnityEngine;

namespace MSZ_Aftermath.Injections.Overrides
{
    public class LUI : MonoBehaviour
    {
        private void Start()
        {
            if (Dedicator.isDedicated)
            {

            }
            else
            {
                GameObject.Destroy(gameObject);
            }
        }

        private void Awake()
        {
        }

        private void OnDestroy()
        {
        }

        private void OnGUI()
        {
        }

        public static void assetsLoad(string key, int count, float progress, float step)
        {
        }

        public static void assetsScan(string key, int count)
        {
        }

        private void loadBackgroundImage()
        {
            return;
        }

        private void OnLevelWasLoaded(int id)
        {
        }

        private static void onQueuePositionUpdated()
        {
        }

        public static void rebuild()
        {
        }

        public static void updateKey(string key)
        {
        }

        public static void updateProgress(float progress)
        {
        }

        public static void updateScene()
        {
        }
    }
}
