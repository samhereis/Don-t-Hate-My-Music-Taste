using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class RoomUnit : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _roomName;
        [SerializeField] private Button _joinRoom;

        private RoomInfo _roomInfo;

        private void Awake()
        {
            _joinRoom.onClick.AddListener(JoinRoom);
        }

        private void OnDestroy()
        {
            _joinRoom.onClick.RemoveListener(JoinRoom);
        }

        public void SetDetails(RoomInfo roomInfo)
        {
            _roomInfo = roomInfo;
            _roomName.text = roomInfo.Name;

            try
            {
                _roomName.text += " - " + roomInfo.CustomProperties["SceneName"];
            }
            finally
            {
                
            }
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(_roomInfo.Name);

            LoadingCanvas.instance.SetText("Joining Room");
            LoadingCanvas.instance.Open();
        }
    }
}