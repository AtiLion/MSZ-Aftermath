using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;

namespace MSZ_Aftermath
{
    public class Information
    {
        public static Player player = null;
        public static bool trustClient = false;
        public static List<SteamPlayer> hackers = new List<SteamPlayer>();
        public static bool beingScreened = false;
        public static Color esp_zombies = Color.red;
        public static Color esp_players = Color.green;
        public static Color esp_vehicles = Color.yellow;
        public static Color esp_items = Color.magenta;
        public static Color esp_storages = new Color(1f, 1f, 0, 0);
        public static Color esp_friends = Color.blue;
        public static Color esp_animals = new Color(1, 1f, 0, 0);
        public static Color esp_beds = new Color(1f, 0, 1f, 0);

        public static Zombie[] getZombies()
        {
            List<Zombie> temp = new List<Zombie>();
            for (int i = 0; i < ZombieManager.regions.Length; i++)
            {
                temp.AddRange(ZombieManager.regions[i].zombies);
            }
            return temp.ToArray();
        }

        public static SteamPlayer[] getPlayers()
        {
            return Provider.clients.ToArray();
        }

        public static Animal[] getAnimals()
        {
            return AnimalManager.animals.ToArray();
        }

        public static InteractableVehicle[] getVehicles()
        {
            return VehicleManager.vehicles.ToArray();
        }

        public static InteractableItem[] getItems()
        {
            return UnityEngine.Object.FindObjectsOfType(typeof(InteractableItem)) as InteractableItem[];
        }

        public static InteractableStorage[] getStorages()
        {
            return UnityEngine.Object.FindObjectsOfType(typeof(InteractableStorage)) as InteractableStorage[];
        }

        public static InteractableBed[] getBeds()
        {
            return UnityEngine.Object.FindObjectsOfType(typeof(InteractableBed)) as InteractableBed[];
        }
    }
}
