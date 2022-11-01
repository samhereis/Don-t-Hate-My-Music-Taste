using Helpers;
using UnityEngine;

namespace UI.Helpers
{
    public class LeftAndRightByScreenWidth : MonoBehaviour
    {
        [SerializeField] private RectTransform _parent;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _left;
        [SerializeField] private float _right;

        private void OnValidate()
        {
            Setup();
        }

        private void Update()
        {
            if (_rectTransform != null)
            {
                if (_parent != null)
                {
                    _rectTransform.SetLeft(NumberHelper.GetNumberFromPercentage(_parent.rect.width, _left));
                    _rectTransform.SetRight(NumberHelper.GetNumberFromPercentage(_parent.rect.width, _right));
                }
                else
                {
                    _rectTransform.SetLeft(NumberHelper.GetNumberFromPercentage(Screen.width, _left));
                    _rectTransform.SetRight(NumberHelper.GetNumberFromPercentage(Screen.width, _right));
                }
            }
        }

        private void Setup()
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
                this.TrySetDirty();
            }

            if (_parent == null)
            {
                _parent = transform.parent.GetComponent<RectTransform>();
                this.TrySetDirty();
            }
        }
    }
}