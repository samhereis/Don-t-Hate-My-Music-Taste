using System.Collections;
using System.Collections.Generic;
using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window
{
    public class Multiplayer_MainWindow : CanvasWindowBase
    {
        [Header("Windows")]
        [SerializeField] private Multiplayer_JoinRoomWindow _joinRoomWindow;
        [SerializeField] private Multiplayer_CreateRoomWindow _createRoomWindow;

        [Header("Buttons")]
        [SerializeField] private Button _joinRoomButton;
        [SerializeField] private Button _createRoomButton;

        private void Start()
        {
            _joinRoomButton.onClick.AddListener(OnJoinRoomButtonClicked);
            _createRoomButton.onClick.AddListener(OnJoinRoomButtonClicked);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _joinRoomButton.onClick.RemoveListener(OnJoinRoomButtonClicked);
            _createRoomButton.onClick.AddListener(OnJoinRoomButtonClicked);
        }

        private void OnJoinRoomButtonClicked()
        {
            _joinRoomWindow?.Open();
        }

        private void OnCreateRoomButtonClicked()
        {
            _createRoomWindow?.Open();
        }
    }
}