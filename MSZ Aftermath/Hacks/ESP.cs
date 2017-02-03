using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using HighlightingSystem;
using MSZ_Aftermath.Types;

namespace MSZ_Aftermath.Hacks
{
    public class ESP : MenuUI
    {
        private Library.DrawManager dManager;

        private Thread t_ESP;

        private bool terminate = false;

        public bool espEnabled = false;
        public bool draw_outline = true;
        public bool draw_highlight = false;
        public bool esp_player = true;
        public bool p_name = true;
        public bool p_distance = true;
        public bool esp_zombie = false;
        public bool z_name = false;
        public bool z_distance = false;
        public bool esp_animal = false;
        public bool a_name = false;
        public bool a_distance = false;
        public bool esp_item = false;
        public bool i_name = false;
        public bool i_distance = false;
        public bool i_ignorefilter = false;
        public bool esp_vehicle = false;
        public bool v_name = false;
        public bool v_distance = false;
        public bool esp_storage = false;
        public bool s_name = false;
        public bool s_distance = false;
        public bool s_ignorelocked = false;
        public bool esp_bed = false;
        public bool b_name = false;
        public bool b_distance = false;
        public bool b_ignoreused = false;
        public float distance = 1000f;

        public ESP()
            : base("ESP Menu", true, false, false)
        {
        }

        public void Start()
        {
            dManager = ComponentManager.draw_manager;

            t_ESP = new Thread(new ThreadStart(udESP));
            t_ESP.Start();
        }

        public void Update()
        {
        }

        private void udESP()
        {
            while (!terminate)
            {
                try
                {
                    if (espEnabled)
                    {
                        if (dManager.d_ref)
                        {
                            dManager.drawing_esp.Clear();
                            Collider[] cols = Physics.OverlapSphere(Information.player.transform.position, distance, RayMasks.DAMAGE_CLIENT);
                            foreach (Collider c in cols)
                            {
                                if (DamageTool.getPlayer(c.transform) != null)
                                {
                                    SDG.Unturned.Player ut = DamageTool.getPlayer(c.transform);
                                    if (esp_player)
                                    {
                                        if (ut.gameObject == null)
                                            continue;
                                        SteamPlayer ply = Tool.getSteamPlayer(ut.gameObject);
                                        bool isFriend = ComponentManager.friend_manager.exists(ply.playerID.steamID.m_SteamID);
                                        float dist = (float)Math.Round(Tool.getDistance(ut.transform.position));
                                        Vector3 c2s = MainCamera.instance.WorldToScreenPoint(ut.transform.position);
                                        Highlighter h = ut.gameObject.GetComponent<Highlighter>();

                                        if (c2s.z <= 0)
                                            continue;

                                        c2s.x = c2s.x - 64f;
                                        c2s.y = (Screen.height - (c2s.y + 1f)) - 12f;

                                        if (Array.Exists(dManager.drawing_esp.ToArray(), a => a.pos == c2s))
                                            continue;

                                        dManager.drawing_esp.Add(new DrawType((p_name ? ply.playerID.nickName : "") + (p_distance ? "[" + dist + "]" : ""), c2s, (isFriend ? Information.esp_friends : Information.esp_players), p_name || p_distance, ut.gameObject, Tool.BoundsToScreenRect(c.bounds)));


                                        if (draw_highlight)
                                        {
                                            if (h == null)
                                            {
                                                h = ut.gameObject.AddComponent<Highlighter>();
                                                h.OccluderOn();
                                                h.SeeThroughOn();
                                                h.ConstantOn((isFriend ? Information.esp_friends : Information.esp_players));
                                            }
                                            HighlightType ht = dManager.highlights.Find(a => a.h == h);
                                            if (ht == null)
                                                dManager.highlights.Add(new HighlightType(h, ut.gameObject));
                                            else
                                                ht.hasUd = true;
                                        }
                                        continue;
                                    }
                                }

                                if (DamageTool.getZombie(c.transform))
                                {
                                    SDG.Unturned.Zombie ut = DamageTool.getZombie(c.transform);
                                    if (esp_zombie)
                                    {
                                        if (ut.gameObject == null && !ut.isDead)
                                            continue;
                                        float dist = (float)Math.Round(Tool.getDistance(ut.transform.position));
                                        Vector3 c2s = Camera.main.WorldToScreenPoint(ut.transform.position);
                                        Highlighter h = ut.gameObject.GetComponent<Highlighter>();

                                        if (c2s.z <= 0)
                                            continue;

                                        c2s.x = c2s.x - 64f;
                                        c2s.y = (Screen.height - (c2s.y + 1f)) - 12f;

                                        if (Array.Exists(dManager.drawing_esp.ToArray(), a => a.pos == c2s))
                                            continue;

                                        dManager.drawing_esp.Add(new DrawType((z_name ? getZombieName(ut) : "") + (z_distance ? "[" + dist + "]" : ""), c2s, Information.esp_zombies, z_name || z_distance, ut.gameObject, Tool.BoundsToScreenRect(c.bounds)));

                                        if (draw_highlight)
                                        {
                                            if (h == null)
                                            {
                                                h = ut.gameObject.AddComponent<Highlighter>();
                                                h.OccluderOn();
                                                h.SeeThroughOn();
                                                h.ConstantOn(Information.esp_zombies);
                                            }
                                            HighlightType ht = dManager.highlights.Find(a => a.h == h);
                                            if (ht == null)
                                                dManager.highlights.Add(new HighlightType(h, ut.gameObject));
                                            else
                                                ht.hasUd = true;
                                        }
                                        continue;
                                    }
                                }
                            }
                            dManager.d_ref = false;
                        }
                    }
                    else
                    {
                        dManager.rm_highlights = true;
                        dManager.drawing_esp.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        public void OnDestroy()
        {
            terminate = true;
        }

        private string getZombieName(Zombie z)
        {
            if (z.speciality == EZombieSpeciality.ACID)
            {
                return "Acid Zombie";
            }
            else if (z.speciality == EZombieSpeciality.BURNER)
            {
                return "Burner Zombie";
            }
            else if (z.speciality == EZombieSpeciality.CRAWLER)
            {
                return "Crawler Zombie";
            }
            else if (z.speciality == EZombieSpeciality.FLANKER_FRIENDLY)
            {
                return "Friendly Flanker Zombie";
            }
            else if (z.speciality == EZombieSpeciality.FLANKER_STALK)
            {
                return "Stalker Zombie";
            }
            else if (z.speciality == EZombieSpeciality.MEGA)
            {
                return "Mega Zombie";
            }
            else if (z.speciality == EZombieSpeciality.SPRINTER)
            {
                return "Spitter Zombie";
            }
            else
            {
                return "Normal Zombie";
            }
        }

        private string getAnimalName(Animal a)
        {
            return a.asset.animalName;
        }

        public override void loadUI()
        {
            espEnabled = GUILayout.Toggle(espEnabled, "ESP");
            draw_outline = GUILayout.Toggle(draw_outline, "Draw Outlines");
            draw_highlight = GUILayout.Toggle(draw_highlight, "Draw Highlights(buggy as shit)");
            esp_player = GUILayout.Toggle(esp_player, "Players");
            p_name = GUILayout.Toggle(p_name, "Player Names");
            p_distance = GUILayout.Toggle(p_distance, "Player Distance");
            esp_zombie = GUILayout.Toggle(esp_zombie, "Zombies");
            z_name = GUILayout.Toggle(z_name, "Zombine Names");
            z_distance = GUILayout.Toggle(z_distance, "Zombie Distance");
            esp_vehicle = GUILayout.Toggle(esp_vehicle, "Vehicles");
            v_name = GUILayout.Toggle(v_name, "Vehicle Names");
            v_distance = GUILayout.Toggle(v_distance, "Vehicle Distance");
            esp_item = GUILayout.Toggle(esp_item, "Items");
            i_name = GUILayout.Toggle(i_name, "Item Names");
            i_distance = GUILayout.Toggle(i_distance, "Item Distance");
            i_ignorefilter = GUILayout.Toggle(i_ignorefilter, "Ignore Item Filter");
            esp_animal = GUILayout.Toggle(esp_animal, "Animals");
            a_name = GUILayout.Toggle(a_name, "Animal Names");
            a_distance = GUILayout.Toggle(a_distance, "Animal Distance");
            esp_storage = GUILayout.Toggle(esp_storage, "Storages");
            s_name = GUILayout.Toggle(s_name, "Storage Names");
            s_distance = GUILayout.Toggle(s_distance, "Storage Distance");
            s_ignorelocked = GUILayout.Toggle(s_ignorelocked, "Ignore Locked Storages");
            esp_bed = GUILayout.Toggle(esp_bed, "Beds");
            b_name = GUILayout.Toggle(b_name, "Bed Owner");
            b_distance = GUILayout.Toggle(b_distance, "Bed Distance");
            b_ignoreused = GUILayout.Toggle(b_ignoreused, "Ignore Owned Beds");
            GUILayout.Label("Distance: " + distance);
            distance = GUILayout.HorizontalSlider((float)Math.Round(distance, 0), 0f, 4000f);
            if (GUILayout.Button("Item Type Filter"))
            {
                ComponentManager.hack_Main.sHid = true;
                ComponentManager.hack_Main.hPos = 1;
            }
        }
    }
}
