using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SDG.Unturned;
using Steamworks;

namespace MSZ_Aftermath.Injections.Overrides
{
    public class PR : MonoBehaviour
    {
        private void OnApplicationQuit() // Override_Assembly-CSharp | SDG.Unturned.Provider | NoDiff | Insta
        {
            if (!Provider.isInitialized)
            {
                return;
            }
            if (!Universal.altf4)
            {
                if (!Provider.isServer && Provider.isPvP && Provider.clients.Count > 1 && Player.player != null && !Player.player.movement.isSafe && !Player.player.life.isDead)
                {
                    Application.CancelQuit();
                    return;
                }
            }
            Provider.disconnect();
            Provider.provider.shutdown();
        }

        private static int maxRetryAmount = 3;

        private static void changeField(string name, object value)
        {
            typeof(Provider).GetField(name, BindingFlags.NonPublic | BindingFlags.Static).SetValue(null, value);
        }

        private static object getField(string name)
        {
            return typeof(Provider).GetField(name, BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
        }

        private static object runMethod(string name, object[] args)
        {
            return typeof(Provider).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, args);
        }

        public static void send(CSteamID steamID, ESteamPacket type, byte[] packet, int size, int channel) // Override
        {
            if (!Provider.isConnected)
            {
                return;
            }
            changeField("_bytesSent", (uint)getField("_bytesSent") + (uint)size);
            changeField("_packetsSent", (uint)getField("_packetsSent") + 1u);
            if (Provider.isServer)
            {
                if (steamID == Provider.server || (Provider.isClient && steamID == Provider.client))
                {
                    runMethod("receiveServer", new object[]{
                        Provider.server,
                        packet,
                        0,
                        size,
                        channel,
                    });
                    return;
                }
                if (steamID.m_SteamID == 0uL)
                {
                    Debug.LogError("Failed to send to invalid steam ID.");
                    return;
                }
                if ((bool)runMethod("isUnreliable", new object[] { type }))
                {
                    if (!SteamGameServerNetworking.SendP2PPacket(steamID, packet, (uint)size, (!(bool)runMethod("isInstant", new object[] { type })) ? EP2PSend.k_EP2PSendUnreliable : EP2PSend.k_EP2PSendUnreliableNoDelay, channel))
                    {
                        Debug.LogError(string.Concat(new object[]
				        {
					        "Failed to send size ",
					        size,
					        " unreliable packet to ",
					        steamID,
					        "!"
				        }));
                    }
                    return;
                }
                if (!SteamGameServerNetworking.SendP2PPacket(steamID, packet, (uint)size, (!(bool)runMethod("isInstant", new object[] { type })) ? EP2PSend.k_EP2PSendReliableWithBuffering : EP2PSend.k_EP2PSendReliable, channel))
                {
                    Debug.LogError(string.Concat(new object[]
			        {
				        "Failed to send size ",
				        size,
				        " reliable packet to ",
				        steamID,
				        "!"
			        }));
                }
            }
            else
            {
                if (steamID == Provider.client)
                {
                    runMethod("receiveClient", new object[]{
                        Provider.client,
                        packet,
                        0,
                        size,
                        channel,
                    });
                    Provider._connectionFailureInfo = ESteamConnectionFailureInfo.LATE_PENDING;
                    Provider.disconnect();
                    return;
                }
                if (steamID.m_SteamID == 0uL)
                {
                    Debug.LogError("Failed to send to invalid steam ID.");
                    Provider._connectionFailureInfo = ESteamConnectionFailureInfo.LATE_PENDING;
                    Provider.disconnect();
                    return;
                }
                if ((bool)runMethod("isUnreliable", new object[] { type }))
                {
                    bool suc = false;
                    for (int i = 0; i < maxRetryAmount; i++)
                    {
                        if (!SteamNetworking.SendP2PPacket(steamID, packet, (uint)size, (!(bool)runMethod("isInstant", new object[] { type })) ? EP2PSend.k_EP2PSendUnreliable : EP2PSend.k_EP2PSendUnreliableNoDelay, channel))
                        {
                            Debug.LogError(string.Concat(new object[]
				            {
					            "Failed to send size ",
					            size,
					            " unreliable packet to ",
					            steamID,
					            "!"
				            }));
                        }
                        else
                        {
                            suc = true;
                            break;
                        }
                    }
                    if (!suc)
                    {
                        Provider._connectionFailureInfo = ESteamConnectionFailureInfo.LATE_PENDING;
                        Provider.disconnect();
                    }
                    return;
                }
                bool suc1 = false;
                for (int i = 0; i < maxRetryAmount; i++)
                {
                    if (!SteamNetworking.SendP2PPacket(steamID, packet, (uint)size, (!(bool)runMethod("isInstant", new object[] { type })) ? EP2PSend.k_EP2PSendReliableWithBuffering : EP2PSend.k_EP2PSendReliable, channel))
                    {
                        Debug.LogError(string.Concat(new object[]
			            {
				            "Failed to send size ",
				            size,
				            " reliable packet to ",
				            steamID,
				            "!"
			            }));
                    }
                    else
                    {
                        suc1 = true;
                        break;
                    }
                    if (!suc1)
                    {
                        Provider._connectionFailureInfo = ESteamConnectionFailureInfo.LATE_PENDING;
                        Provider.disconnect();
                    }
                }
            }
        }
    }
}
