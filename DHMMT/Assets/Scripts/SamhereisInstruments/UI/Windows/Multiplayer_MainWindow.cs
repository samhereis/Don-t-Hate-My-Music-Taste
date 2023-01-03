using System.Collections;
using System.Collections.Generic;
using Helpers;
using Photon.Pun;
using TMPro;
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
        [SerializeField] private Button _setNickname;

        [Header("Components")]
        [SerializeField] private TMP_InputField _nicknameText;

        private void Start()
        {
            _joinRoomButton.onClick.AddListener(OnJoinRoomButtonClicked);
            _createRoomButton.onClick.AddListener(OnCreateRoomButtonClicked);
            _setNickname.onClick.AddListener(SetNickname);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _joinRoomButton.onClick.RemoveListener(OnJoinRoomButtonClicked);
            _createRoomButton.onClick.RemoveListener(OnCreateRoomButtonClicked);
            _setNickname.onClick.RemoveListener(SetNickname);
        }

        private void OnJoinRoomButtonClicked()
        {
            _joinRoomWindow?.Open();
        }

        private void OnCreateRoomButtonClicked()
        {
            _createRoomWindow?.Open();
        }

        public void SetNickname()
        {
            if (StringHelper.IsNickName(_nicknameText.text))
            {
                PhotonNetwork.NickName = _nicknameText.text;
            }
        }
    }
}