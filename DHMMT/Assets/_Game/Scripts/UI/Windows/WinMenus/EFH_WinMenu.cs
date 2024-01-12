using UI.Canvases;
using UnityEngine;

namespace UI.Windows
{
    public class EFH_WinMenu : MenuExtenderBase<WinMenu>
    {
        private void Awake()
        {
            window.onSubscribeToEvents += OnSubscribeToEvents;
            window.onUnsubscribeFromEvents += OnUnsubscribeFromEvents;
        }

        private void OnDestroy()
        {
            window.onSubscribeToEvents -= OnSubscribeToEvents;
            window.onUnsubscribeFromEvents -= OnUnsubscribeFromEvents;
        }

        private void OnSubscribeToEvents()
        {
        }

        private void OnUnsubscribeFromEvents()
        {

        }
    }
}