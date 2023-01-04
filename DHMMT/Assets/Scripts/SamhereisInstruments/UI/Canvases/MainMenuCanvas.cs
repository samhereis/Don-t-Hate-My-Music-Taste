using System.Collections;
using System.Collections.Generic;
using Network;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    public class MainMenuCanvas : CanvasBase
    {
        [Header("Canvases")]
        [SerializeField] private LoadingCanvas _loadingCanvas;
        [SerializeField] private MultiplayerCanvas _multiplayerCanvas;

        [Header("Buttons")]
        [SerializeField] private Button _playMultiplayerButton;

        private void Start()
        {
            Subscribe();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            UnSubscribe();
        }

        private void Subscribe()
        {
            UnSubscribe();

            _playMultiplayerButton.onClick.AddListener(PlayerMultiplayer);

            NetworkEvents.onConnectedToMaster += OnConnectedToMaster;
            NetworkEvents.onJoinedLobby += OnJoinedLobby;
            NetworkEvents.onLeftLobby += OnLeftLobby;
        }

        private void UnSubscribe()
        {
            _playMultiplayerButton.onClick.RemoveListener(PlayerMultiplayer);

            NetworkEvents.onConnectedToMaster -= OnConnectedToMaster;
            NetworkEvents.onJoinedLobby -= OnJoinedLobby;
            NetworkEvents.onLeftLobby -= OnLeftLobby;
        }

        private void PlayerMultiplayer()
        {
            _loadingCanvas?.SetText("Connecting...");
            _loadingCanvas?.Open();

            if (PhotonNetwork.ConnectUsingSettings() == false)
            {
                Open();
            }
        }

        private void OnConnectedToMaster()
        {
            _loadingCanvas?.SetText("Joining lobby...");

            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void OnJoinedLobby()
        {
            _multiplayerCanvas?.Open();
        }

        private void OnLeftLobby()
        {
            Open();
        }
    }
}