using System.Collections;
using System.Collections.Generic;
using Identifiers;
using Photon.Pun;
using UnityEngine;

namespace Network
{
    public class PlayerSpawner : MonoBehaviour
    {
        public static PlayerSpawner instance;

        [SerializeField] private PlayerIdentifier _playerPrefab;

        private void Awake()
        {
            instance = this;
        }

        public void SpawnPlayer()
        {
            PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity);
        }
    }
}
