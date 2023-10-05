using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class EFH_LoseMenu : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _replayButton;

        [Header("Windows")]
        [SerializeField] private LoseMenu _loseMenu;

        private void Awake()
        {
            _loseMenu = GetComponent<LoseMenu>();

            _loseMenu.onSubscribeToEvents += OnSubscribeToEvents;
            _loseMenu.onUnsubscribeFromEvents += OnUnsubscribeFromEvents;
        }

        private void OnDestroy()
        {
            _loseMenu.onSubscribeToEvents -= OnSubscribeToEvents;
            _loseMenu.onUnsubscribeFromEvents -= OnUnsubscribeFromEvents;
        }

        private void OnSubscribeToEvents()
        {
            _mainMenuButton.onClick.AddListener(_loseMenu.GoToMainMenu);
            _replayButton.onClick.AddListener(_loseMenu.Replay);
        }

        private void OnUnsubscribeFromEvents()
        {
            _mainMenuButton.onClick.RemoveListener(_loseMenu.GoToMainMenu);
            _replayButton.onClick.RemoveListener(_loseMenu.Replay);
        }
    }
}