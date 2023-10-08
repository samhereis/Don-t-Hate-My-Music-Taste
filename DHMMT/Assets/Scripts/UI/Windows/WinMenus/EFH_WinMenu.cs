using UnityEngine;

namespace UI.Windows
{
    public class EFH_WinMenu : MonoBehaviour
    {
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
        }

        private void OnUnsubscribeFromEvents()
        {

        }
    }
}