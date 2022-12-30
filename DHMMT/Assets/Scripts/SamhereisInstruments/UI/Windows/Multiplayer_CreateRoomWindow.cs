using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window
{
    public class Multiplayer_CreateRoomWindow : CanvasWindowBase
    {
        [Header("Buttons")]
        [SerializeField] private Button _createRoomButton;

        [Header("Other UI Elements")]
        [SerializeField] private TextMeshProUGUI _roomNameText;

        private void Start()
        {
            _createRoomButton.onClick.AddListener(CreateRoom);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _createRoomButton.onClick.RemoveListener(CreateRoom);
        }

        private void CreateRoom()
        {
            if (_roomNameText.text.Length < 5)
            {
                MessageToUser.instance?.LogError("room name must be more than 5 characters");
                return;
            }

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(_roomNameText.text, roomOptions);
        }
    }
}