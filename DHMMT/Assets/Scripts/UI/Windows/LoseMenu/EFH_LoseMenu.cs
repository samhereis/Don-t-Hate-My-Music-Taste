using UI.Canvases;

namespace UI.Windows
{
    public class EFH_LoseMenu : CanvasWindowExtendorBase<LoseMenu>
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