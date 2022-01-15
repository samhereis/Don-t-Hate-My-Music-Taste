using DG.Tweening;
namespace UI.Window
{
    public class WindowBehavior_ScaleWindow : WindowBehaviorBase
    {
        public override void Open()
        {
            _windowEvents.onOpenStart?.Invoke();

            gameObject.SetActive(true);
            transform.DOScale(1, 0.5f).OnComplete(() => _windowEvents.onOpenEnd?.Invoke()).SetUpdate(true);
        }

        public override void Close()
        {
            _windowEvents.onCloseStart?.Invoke();

            transform.DOScale(0, 0.5f).OnComplete(() => { gameObject.SetActive(false); _windowEvents.onCloseEnd?.Invoke(); }).SetUpdate(true);
        }
    }
}