using System.Collections;
using System.Collections.Generic;
using Network;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;
using UI.Elements;

namespace UI.Window
{
    public class Multiplayer_RoomWindow : CanvasWindowBase
    {
        [Header("Windows")]
        [SerializeField] private CanvasWindowBase _mainMenu;

        [Header("Buttons")]
        [SerializeField] private Button _exitRoomButton;
        [SerializeField] private Button _startGameButton;

        [Header("Other UI Elements")]
        [SerializeField] private TextMeshProUGUI _roomNameText;
        [SerializeField] private Transform _roomPlayerContent;

        [Header("Prefabs")]
        [SerializeField] private RoomPlayerUnit _roomPlayerUnit;

        public override void Enable(float? duration = null)
        {
            base.Enable(duration);

            _roomNameText.text = PhotonNetwork.CurrentRoom.Name;

            _exitRoomButton.onClick.AddListener(LeaveRoom);
            UpdateStartGameButton();

            NetworkEvents.onLeftRoom += OnLeftRoom;
            NetworkEvents.onPlayerEnterRoom += OnPlayerAdded;
            NetworkEvents.onPlayerLeaveRoom += DestroyPlayer;
            NetworkEvents.onMasterSwitched += OnMasterSwitched;

            UpdatePlayers();
        }

        public override void Disable(float? duration = null)
        {
            base.Disable(duration);

            _exitRoomButton.onClick.RemoveListener(LeaveRoom);
            _startGameButton.onClick.RemoveListener(StartGame);

            NetworkEvents.onLeftRoom -= OnLeftRoom;
            NetworkEvents.onPlayerEnterRoom -= OnPlayerAdded;
            NetworkEvents.onPlayerLeaveRoom -= DestroyPlayer;
            NetworkEvents.onMasterSwitched -= OnMasterSwitched;

            ClearPlayers();
        }

        private void StartGame()
        {
            PhotonNetwork.LoadLevel("Escape From Haters");
        }

        private void UpdatePlayers()
        {
            ClearPlayers();

            foreach (var player in PhotonNetwork.PlayerList)
            {
                var playerInstance = Instantiate(_roomPlayerUnit, _roomPlayerContent);
                playerInstance.SetDetails(player);
            }
        }

        private void ClearPlayers()
        {
            foreach (var playerUnit in _roomPlayerContent.GetComponentsInChildren<RoomPlayerUnit>(true))
            {
                DestroyPlayer(playerUnit.assossiatedPlayer);
            }
        }

        private void OnPlayerAdded(Player player)
        {
            UpdatePlayers();
            UpdateStartGameButton();
        }

        private void OnMasterSwitched(Player player)
        {
            UpdateStartGameButton();
        }

        private void DestroyPlayer(Player player)
        {
            foreach (var playerUnit in _roomPlayerContent.GetComponentsInChildren<RoomPlayerUnit>(true))
            {
                if (playerUnit.assossiatedPlayer == player) Destroy(playerUnit.gameObject);
            }
        }

        private void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            LoadingCanvas.instance.SetText("LeavingRoom");
            LoadingCanvas.instance.Open();
        }

        private void OnLeftRoom()
        {
            NetworkEvents.onLeftRoom -= OnLeftRoom;
            _mainMenu?.Open();
        }

        private void UpdateStartGameButton()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
            if (PhotonNetwork.IsMasterClient) _startGameButton.onClick.AddListener(StartGame);
            _startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        }
    }
}