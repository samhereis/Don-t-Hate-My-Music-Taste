using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    public class MultiplayerCanvas : CanvasBase
    {
        [SerializeField] private MainMenuCanvas _mainMenu;

        [SerializeField] private Button _goToMainMenu;

        private void Start()
        {
            _goToMainMenu.onClick.AddListener(GoToMainMenu);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _goToMainMenu.onClick.RemoveListener(GoToMainMenu);
        }

        private void GoToMainMenu()
        {
            PhotonNetwork.Disconnect();
            _mainMenu?.Open();
        }
    }
}