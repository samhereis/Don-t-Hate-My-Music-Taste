using System.Collections;
using System.Collections.Generic;
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
            _playMultiplayerButton.onClick.RemoveListener(PlayerMultiplayer);
            _playMultiplayerButton.onClick.AddListener(PlayerMultiplayer);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _playMultiplayerButton.onClick.RemoveListener(PlayerMultiplayer);
        }

        private void PlayerMultiplayer()
        {
            _loadingCanvas?.SetText("ConnectingToServer");
            _loadingCanvas?.Open();

            if (PhotonNetwork.ConnectUsingSettings() == false)
            {
                Open();
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");

            _multiplayerCanvas?.Open();
        }
    }
}