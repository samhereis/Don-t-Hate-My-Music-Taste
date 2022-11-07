using Helpers;
using Identifiers;
using Mirror;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Network
{
    public class PlayersContainer : MonoBehaviour
    {
        private const string _playerIDPrefix = "Player_";
        private static Dictionary<string, PlayerIdentifier> _player = new Dictionary<string, PlayerIdentifier>();

        public static void RegisterPlayer(string netID, PlayerIdentifier playerIdentifier)
        {
            _player.Add(GetPlayerString(netID), playerIdentifier);
        }

        public static void DeregisterPlayer(string netID, PlayerIdentifier playerIdentifier)
        {
            _player.Remove(GetPlayerString(netID));
        }

        public static async Task<PlayerIdentifier> GetPlayer(string netID)
        {
            PlayerIdentifier playerIdentifier = null;

            try
            {
                playerIdentifier = _player[GetPlayerString(netID)];
            }
            finally
            {
                if (playerIdentifier == null)
                {
                    foreach (NetworkIdentity networkIdentity in FindObjectsOfType<NetworkIdentity>())
                    {
                        await AsyncHelper.Delay();

                        if (networkIdentity.netId.ToString() == GetPlayerString(netID))
                        {
                            playerIdentifier = networkIdentity.GetComponent<PlayerIdentifier>();
                            break;
                        }
                    }
                }
            }

            return playerIdentifier;
        }

        private static string GetPlayerString(string netID)
        {
            return _playerIDPrefix + netID;
        }
    }
}