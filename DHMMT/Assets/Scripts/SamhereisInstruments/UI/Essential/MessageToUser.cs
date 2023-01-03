using DG.Tweening;
using Helpers;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class MessageToUser : MonoBehaviour
    {
        public static MessageToUser instance;

        [Header("Errors")]
        [SerializeField] private RectTransform _messageRect;
        [field: SerializeField] public RectTransform roomNameIsTooShort;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _errorText;
        [SerializeField] private TextMeshProUGUI _messageText;

        [Header("Settings")]
        [SerializeField] private float _showYPosition = -200;
        [SerializeField] private float _hideYPosition = 200;
        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private float _showDuration = 2f;
        [SerializeField] private Ease _ease;

        [Header("Debug")]
        [SerializeField] private bool _isShowingMessage = false;

        private void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(this);
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public async void Log(RectTransform messageToShow)
        {
            await AsyncHelper.Delay();

            ShowUpLog(messageToShow);
        }

        public async void Log(string message)
        {
            await AsyncHelper.Delay();

            _messageText.text = message;
            ShowUpLog(_messageRect);
        }

        private void ShowUpLog(RectTransform messageToShow)
        {
            if (_isShowingMessage == false)
            {
                _isShowingMessage = true;

                messageToShow.gameObject.SetActive(true);

                messageToShow.DOKill();
                messageToShow.DOAnchorPos3DY(_showYPosition, _animationDuration).SetEase(_ease).OnComplete(async () =>
                {
                    await AsyncHelper.Delay(_showDuration);
                    Hide(messageToShow);

                    _isShowingMessage = false;
                });
            }
        }

        private void Hide(RectTransform messageToShow)
        {
            messageToShow.DOKill();
            messageToShow.DOAnchorPos3DY(_hideYPosition, _animationDuration).SetEase(_ease).OnComplete(() =>
            {
                messageToShow.gameObject.SetActive(false);
            });
        }
    }
}