﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath
{
    public class Tool
    {
        public static SteamPlayer getSteamPlayer(GameObject g)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.player.gameObject == g);
        }

        public static SteamPlayer getSteamPlayer(ulong id)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.playerID.steamID.m_SteamID == id);
        }

        public static SteamPlayer getSteamPlayer(string nick)
        {
            return Array.Find(Provider.clients.ToArray(), a => a.playerID.nickName == nick);
        }

        public static float getDistance(Vector3 point)
        {
            return Vector3.Distance(Camera.main.transform.position, point);
        }

        public static void DrawLabel(Vector3 point, string label, float fromY = 0f)
        {
            GUI.Label(new Rect(point.x, point.y + fromY, (float)label.Length * 10f, 24f), label);
        }

        public static bool getLookingAt(out RaycastHit result, float distance = Mathf.Infinity)
        {
            return Physics.Raycast(MainCamera.instance.transform.position, MainCamera.instance.transform.forward, out result, distance, RayMasks.DAMAGE_CLIENT);
        }

        public static bool isObscured(Transform pos)
        {
            return !Physics.Linecast(MainCamera.instance.transform.position, pos.position, RayMasks.DAMAGE_CLIENT);
        }

        public static void Outline(Rect r, Texture2D lineTex)
        {
            Rect rect = new Rect(r.xMin, r.yMax, r.width, 1f);
            Rect rect2 = new Rect(r.xMin, r.yMin, r.width, 1f);
            Rect rect3 = new Rect(r.xMax, r.yMin, 1f, r.height);
            Rect rect4 = new Rect(r.xMin, r.yMin, 1f, r.height);
            GUI.DrawTexture(rect, lineTex);
            GUI.DrawTexture(rect3, lineTex);
            GUI.DrawTexture(rect2, lineTex);
            GUI.DrawTexture(rect4, lineTex);
        }

        public static Rect BoundsToScreenRect(Bounds b)
        {
            Camera main = Camera.main;
            Vector3[] array = new Vector3[]
			{
				main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
				main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
				main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
				main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z)),
				main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z)),
				main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z)),
				main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z)),
				main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z))
			};
            for (int i = 0; i < array.Length; i++)
            {
                array[i].y = (float)Screen.height - array[i].y;
            }
            Vector3 vector = array[0];
            Vector3 vector2 = array[0];
            for (int j = 1; j < array.Length; j++)
            {
                vector = Vector3.Min(vector, array[j]);
                vector2 = Vector3.Max(vector2, array[j]);
            }
            Rect result = Rect.MinMaxRect(vector.x, vector.y, vector2.x, vector2.y);
            result.xMin = result.xMin - 1f;
            result.xMax = result.xMax + 1f;
            result.yMin = result.yMin - 1f;
            result.yMax = result.yMax + 1f;
            return result;
        }
    }
}
