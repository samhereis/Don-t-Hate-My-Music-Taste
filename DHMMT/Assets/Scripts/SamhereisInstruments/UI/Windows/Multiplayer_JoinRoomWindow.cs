using System.Collections;
using System.Collections.Generic;
using Network;
using Photon.Realtime;
using UI.Canvases;
using UI.Elements;
using UnityEngine;

namespace UI.Window
{
    public class Multiplayer_JoinRoomWindow : CanvasWindowBase
    {
        [Header("Windows")]
        [SerializeField] private CanvasWindowBase _roomWindow;
        [SerializeField] private CanvasWindowBase _mainWindow;

        [Header("Components")]
        [SerializeField] private Transform _roomUnitsContent;

        [Header("Prefabs")]
        [SerializeField] private RoomUnit _roomUnitPrefab;

        public override void Enable(float? duration = null)
        {
            base.Enable(duration);

            Populate(NetworkEvents.rooms);

            NetworkEvents.onRoomListUpdate += Populate;

            NetworkEvents.onJoinedRoom += OnJoinedARoom;
            NetworkEvents.onJoinedRoomFailed += OnJoinedARoomFailed;
        }

        public override void Disable(float? duration = null)
        {
            base.Disable(duration);

            NetworkEvents.onRoomListUpdate -= Populate;

            NetworkEvents.onJoinedRoom -= OnJoinedARoom;
            NetworkEvents.onJoinedRoomFailed -= OnJoinedARoomFailed;

            Clear();
        }

        private void Populate(List<RoomInfo> room)
        {
            Clear();

            foreach (var roomInfo in room)
            {
                var roomUnit = Instantiate(_roomUnitPrefab, _roomUnitsContent);
                roomUnit.SetDetails(roomInfo);
            }
        }

        private void Clear()
        {
            foreach (var roomUnit in _roomUnitsContent.GetComponentsInChildren<RoomUnit>(true))
            {
                Destroy(roomUnit.gameObject);
            }
        }

        private void OnJoinedARoom()
        {
            NetworkEvents.onJoinedRoom -= OnJoinedARoom;
            NetworkEvents.onJoinedRoomFailed -= OnJoinedARoomFailed;

            _roomWindow.Open();
        }

        private void OnJoinedARoomFailed(short returnCode, string message)
        {
            MessageToUser.instance?.Log(returnCode + " - " + message);

            NetworkEvents.onJoinedRoom -= OnJoinedARoom;
            NetworkEvents.onJoinedRoomFailed -= OnJoinedARoomFailed;

            _mainWindow.Open();
        }
    }
}