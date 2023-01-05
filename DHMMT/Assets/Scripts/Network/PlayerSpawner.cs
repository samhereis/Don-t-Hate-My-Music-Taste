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
        [SerializeField] private GameObject _instantiatedPlayer;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (PhotonNetwork.IsConnected)
            {
                SpawnPlayer();
            }
        }

        public void SpawnPlayer()
        {
            _instantiatedPlayer = PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity);
        }

        public void Die()
        {
            PhotonNetwork.Destroy(_instantiatedPlayer);

            SpawnPlayer();
        }
    }
}
