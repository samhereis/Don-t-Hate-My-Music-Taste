using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network
{
    public class MatchManager : MonoBehaviour
    {
        public static MatchManager instance;

        [SerializeField] private AudioSource _auidoSourcePrefab;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if (PhotonNetwork.IsConnected == false)
            {
                PhotonNetwork.Disconnect();
                SceneManager.LoadSceneAsync(0);
            }
            else if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(_auidoSourcePrefab.name, -Vector3.zero, Quaternion.identity);
            }
        }
    }
}