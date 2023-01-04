using Helpers;
using UnityEngine;

namespace UI.Helpers
{
    public class TopAndBottonByScreenHeight : MonoBehaviour
    {
        [SerializeField] private RectTransform _parent;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _top;
        [SerializeField] private float _bottom;

        private void OnGUI()
        {
            Scale();
        }

        private void Update()
        {
            Scale();
        }

        private void Scale()
        {
            if (_rectTransform != null)
            {
                if (_parent != null)
                {
                    _rectTransform.SetBottom(NumberHelper.GetNumberFromPercentage(_parent.rect.height, _bottom));
                    _rectTransform.SetTop(NumberHelper.GetNumberFromPercentage(_parent.rect.height, _top));
                }
                else
                {
                    _rectTransform.SetBottom(NumberHelper.GetNumberFromPercentage(Screen.height, _bottom));
                    _rectTransform.SetTop(NumberHelper.GetNumberFromPercentage(Screen.height, _top));
                }
            }
        }

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
            if (_parent == null) _parent = transform.parent.GetComponent<RectTransform>();

            this.TrySetDirty();
        }
    }
}