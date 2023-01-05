using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Network
{
    public class MusicSyncronization : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] private AudioSource _audioSource;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsReading)
            {
                _audioSource.clip = (AudioClip)stream.ReceiveNext();
                _audioSource.time = (float)stream.ReceiveNext();
            }
            else if (stream.IsWriting)
            {
                stream.SendNext(_audioSource.clip);
                stream.SendNext(_audioSource.time);
            }
        }
    }
}