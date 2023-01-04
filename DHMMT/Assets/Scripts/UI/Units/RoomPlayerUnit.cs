using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class RoomPlayerUnit : MonoBehaviour
    {
        [field: SerializeField] public Player assossiatedPlayer { get; private set; }
        [SerializeField] private TextMeshProUGUI _playerName;

        public void SetDetails(Player player)
        {
            assossiatedPlayer = player;
            _playerName.text = player.NickName;
        }
    }
}