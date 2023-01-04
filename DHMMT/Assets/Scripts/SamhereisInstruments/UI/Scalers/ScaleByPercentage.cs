using Helpers;
using LazyUpdators;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.Helpers
{
    [ExecuteAlways]
    public class ScaleByPercentage : MonoBehaviour
    {
        [SerializeField] private RectTransform _parent;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _top;
        [SerializeField] private float _bottom;
        [SerializeField] private float _left;
        [SerializeField] private float _right;

        private void OnValidate()
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
                this.TrySetDirty();
            }
            if (transform.parent != null)
            {
                _parent = transform.parent.GetComponent<RectTransform>();
                this.TrySetDirty();
            }
        }

        private void Awake()
        {
            if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            DoAutoScale();
        }

        private void DoAutoScale()
        {
            if (_rectTransform != null)
            {
                if (_parent != null)
                {
                    _rectTransform.SetBottom(NumberHelper.GetNumberFromPercentage(_parent.rect.height, _bottom));
                    _rectTransform.SetTop(NumberHelper.GetNumberFromPercentage(_parent.rect.height, _top));

                    _rectTransform.SetLeft(NumberHelper.GetNumberFromPercentage(_parent.rect.width, _left));
                    _rectTransform.SetRight(NumberHelper.GetNumberFromPercentage(_parent.rect.width, _right));
                }
                else
                {
                    _rectTransform.SetBottom(NumberHelper.GetNumberFromPercentage(Screen.height, _bottom));
                    _rectTransform.SetTop(NumberHelper.GetNumberFromPercentage(Screen.height, _top));

                    _rectTransform.SetLeft(NumberHelper.GetNumberFromPercentage(Screen.width, _left));
                    _rectTransform.SetRight(NumberHelper.GetNumberFromPercentage(Screen.width, _right));
                }
            }
        }
    }
}