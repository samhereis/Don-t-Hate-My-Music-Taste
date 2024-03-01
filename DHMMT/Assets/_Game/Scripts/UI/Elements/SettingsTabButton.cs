using Configs;
using DependencyInjection;
using DG.Tweening;
using Helpers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class SettingsTabButton : MonoBehaviour, INeedDependencyInjection
    {
        public Action onClick;

        [Header("Components")]
        [SerializeField] private Transform _activeTabIndicatorParent;
        [SerializeField] private Button _tabButton;

        private float _activeTabIndicatorMoveSpeed => _iuConfigs != null ? _iuConfigs.settingsMenuConfigs.activeTabIndicatorMoveSpeed : 0.25f;
        private Ease _ease => _iuConfigs != null ? _iuConfigs.settingsMenuConfigs.activeTabIndicatorMoveEase : Ease.InOutBack;

        [Inject] private UIConfigs _iuConfigs;

        private void Awake()
        {
            _tabButton ??= GetComponent<Button>();

            DependencyContext.diBox.InjectDataTo(this);
        }

        private void OnEnable()
        {
            _tabButton?.onClick.AddListener(OnClickedButton);
        }

        private void OnDisable()
        {
            _tabButton?.onClick.RemoveListener(OnClickedButton);
        }

        private void OnClickedButton()
        {
            onClick?.Invoke();
        }

        public async void SetActiveTabIndicator(CanvasGroup activeTabIndicator, Transform activeTabIndicatorParent)
        {
            activeTabIndicator.DOKill();

            _tabButton?.onClick.RemoveListener(OnClickedButton);

            activeTabIndicator.transform.SetParent(activeTabIndicatorParent);

            activeTabIndicator.DOFade(0.5f, _activeTabIndicatorMoveSpeed * 0.25f).OnComplete(OnInitialFadeComplete);

            await AsyncHelper.DelayFloat(_activeTabIndicatorMoveSpeed);

            _tabButton?.onClick.RemoveListener(OnClickedButton);
            _tabButton?.onClick.AddListener(OnClickedButton);

            void OnInitialFadeComplete()
            {
                activeTabIndicator.transform
                    .DOMove(_activeTabIndicatorParent.position, _activeTabIndicatorMoveSpeed * 0.5f)
                    .SetEase(_ease)
                    .OnComplete(OnMoveComplete);
            }

            void OnMoveComplete()
            {
                activeTabIndicator.transform.SetParent(_activeTabIndicatorParent);

                var rectTransform = activeTabIndicator.GetComponent<RectTransform>();

                if (rectTransform != null)
                {
                    rectTransform.SetTop(0);
                    rectTransform.SetBottom(0);
                    rectTransform.SetRight(0);
                    rectTransform.SetLeft(0);

                    rectTransform.localScale = Vector3.one;
                }

                activeTabIndicator.DOFade(1, _activeTabIndicatorMoveSpeed * 0.25f);
            }
        }
    }
}