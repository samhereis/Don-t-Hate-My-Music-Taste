using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class EFH_WinMenu : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _replayButton;

        [Header("Windows")]
        [SerializeField] private WinMenu _winMenu;

        private void Awake()
        {
            _winMenu = GetComponent<WinMenu>();

            _winMenu.onSubscribeToEvents += OnSubscribeToEvents;
            _winMenu.onUnsubscribeFromEvents += OnUnsubscribeFromEvents;
        }

        private void OnDestroy()
        {
            _winMenu.onSubscribeToEvents -= OnSubscribeToEvents;
            _winMenu.onUnsubscribeFromEvents -= OnUnsubscribeFromEvents;
        }

        private void OnSubscribeToEvents()
        {
            _mainMenuButton.onClick.AddListener(_winMenu.GoToMainMenu);
            _replayButton.onClick.AddListener(_winMenu.Replay);
        }

        private void OnUnsubscribeFromEvents()
        {
            _mainMenuButton.onClick.RemoveListener(_winMenu.GoToMainMenu);
            _replayButton.onClick.RemoveListener(_winMenu.Replay);
        }
    }
}