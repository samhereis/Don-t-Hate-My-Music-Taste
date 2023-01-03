using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Network
{
    public class NetworkEvents : MonoBehaviourPunCallbacks
    {
        public static Action onConnectedToMaster;

        public static Action onJoinedLobby;
        public static Action onLeftLobby;

        public static Action onJoinedRoom;
        public static Action onLeftRoom;
        public static Action<short, string> onJoinedRoomFailed;

        public static Action<List<RoomInfo>> onRoomListUpdate;

        public static Action<Player> onPlayerEnterRoom;
        public static Action<Player> onPlayerLeaveRoom;

        public static Action<Player> onMasterSwitched;

        [SerializeField] private static List<RoomInfo> _rooms = new List<RoomInfo>();
        public static List<RoomInfo> rooms => _rooms;

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
            onConnectedToMaster?.Invoke();
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
            onJoinedLobby?.Invoke();
        }

        public override void OnLeftLobby()
        {
            Debug.Log("OnLeftLobby");
            onLeftLobby?.Invoke();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");
            onJoinedRoom?.Invoke();
        }

        public override void OnLeftRoom()
        {
            Debug.Log("OnLeftRoom");
            onLeftRoom?.Invoke();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed: " + returnCode + "   " + message);
            onJoinedRoomFailed?.Invoke(returnCode, message);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("OnRoomListUpdate");
            _rooms = roomList;

            onRoomListUpdate?.Invoke(_rooms);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("OnPlayerEnteredRoom");
            onPlayerEnterRoom?.Invoke(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("OnPlayerLeftRoom");
            onPlayerLeaveRoom?.Invoke(otherPlayer);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            onMasterSwitched?.Invoke(newMasterClient);
        }
    }
}