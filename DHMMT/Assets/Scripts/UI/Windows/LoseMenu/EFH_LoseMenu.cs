using UnityEngine;

namespace UI.Windows
{
    public class EFH_LoseMenu : MonoBehaviour
    {
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

        }

        private void OnUnsubscribeFromEvents()
        {

        }
    }
}