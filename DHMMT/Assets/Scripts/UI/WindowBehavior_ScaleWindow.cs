using DG.Tweening;
namespace UI.Window
{
    public class WindowBehavior_ScaleWindow : WindowBehaviorBase
    {
        public override void Open()
        {
            onOpenStart?.Invoke();

            gameObject.SetActive(true);
            transform.DOScale(1, 0.5f).OnComplete(() => onOpenEnd?.Invoke()).SetUpdate(true);
        }

        public override void Close()
        {
            transform.DOScale(0, 0.5f).OnComplete(() => gameObject.SetActive(false)).SetUpdate(true);
        }
    }
}