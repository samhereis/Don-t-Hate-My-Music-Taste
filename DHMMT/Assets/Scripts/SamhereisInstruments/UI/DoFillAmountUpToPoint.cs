using DG.Tweening;
using Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DoFillAmountUpToPoint : MonoBehaviour
    {
        private enum Origin { Left, Right }

        [SerializeField] private Image _filledImage;
        [SerializeField] private Image _fillToThisPoint;
        [SerializeField] private RectTransform _borderHolder;
        [SerializeField] private Origin _origin;

        [Header("Debug")]
        [SerializeField] private float _percent;
        [SerializeField] private Vector3 _wordPos;
        [SerializeField] private float _value;
        [SerializeField] private int _screenWidth;
        [SerializeField] private Camera _camera;

        private Camera camera => _camera != null ? _camera : _camera = FindObjectOfType<Camera>();

        public void Awake()
        {
            _filledImage.fillAmount = 1;

            Close(0);

            _screenWidth = Screen.width;
        }

        public void Close(float duration)
        {
            if (_origin == Origin.Right) _fillToThisPoint.rectTransform.DOAnchorPosX(-_borderHolder.sizeDelta.x / 2, duration); 
            else _fillToThisPoint.rectTransform.DOAnchorPosX(_borderHolder.sizeDelta.x / 2, duration); 
        }

        private async void OnEnable()
        {
            await AsyncHelper.Delay(500);

            _fillToThisPoint.rectTransform.DOAnchorPosX(0, 1);
        }

        private void Update()
        {
            Do();
        }

        private void Do()
        {
            _wordPos = camera.WorldToScreenPoint(_fillToThisPoint.rectTransform.position);

            _value = _wordPos.x;

            if (_origin == Origin.Right) _value -= _screenWidth; 

            _percent = NumberHelper.GetPercentageOf(_value, _screenWidth) / 100;

            _filledImage.fillAmount = Mathf.Abs(_percent);
        }

        [ContextMenu(nameof(Test))] public void Test()
        {
            Do();
        }
    }
}