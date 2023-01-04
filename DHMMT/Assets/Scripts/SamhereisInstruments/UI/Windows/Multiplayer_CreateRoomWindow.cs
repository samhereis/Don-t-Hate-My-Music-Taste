using System.Collections;
using System.Collections.Generic;
using Network;
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
        [Header("Windows")]
        [SerializeField] private CanvasWindowBase _roomWindow;
        [SerializeField] private CanvasWindowBase _mainWindow;

        [Header("Buttons")]
        [SerializeField] private Button _createRoomButton;

        [Header("Other UI Elements")]
        [SerializeField] private TMP_InputField _roomNameText;
        [SerializeField] private TMP_InputField _sceneName;

        [Header("Settings")]
        [SerializeField][Range(2, 10)] private int _maxPlayers;

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
                MessageToUser.instance?.Log(MessageToUser.instance.roomNameIsTooShort);
                return;
            }

            NetworkEvents.onJoinedRoom += OnJoinedARoom;
            NetworkEvents.onJoinedRoomFailed += OnJoinedARoomFailed;

            LoadingCanvas.instance.SetText("Creating room...");
            LoadingCanvas.instance.Open();

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;

            SetCustomPropertiesForLobby(roomOptions);

            roomOptions.CustomRoomProperties.Add("SceneName", _sceneName.text);

            PhotonNetwork.CreateRoom(_roomNameText.text, roomOptions);
        }

        private void OnJoinedARoom()
        {
            NetworkEvents.onJoinedRoom -= OnJoinedARoom;
            NetworkEvents.onJoinedRoomFailed -= OnJoinedARoomFailed;

            _roomWindow?.Open();
        }

        private void OnJoinedARoomFailed(short returnCode, string message)
        {
            MessageToUser.instance?.Log(returnCode + " - " + message);

            NetworkEvents.onJoinedRoom -= OnJoinedARoom;
            NetworkEvents.onJoinedRoomFailed -= OnJoinedARoomFailed;

            _mainWindow?.Open();
        }

        private void SetCustomPropertiesForLobby(RoomOptions roomOptions)
        {
            roomOptions.CustomRoomPropertiesForLobby = new string[1] { "SceneName" };
        }
    }
}