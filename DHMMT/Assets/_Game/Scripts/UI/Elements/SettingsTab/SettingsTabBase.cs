using Configs;
using DependencyInjection;
using DG.Tweening;
using Helpers;
using Interfaces;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace UI.Elements.SettingsTab
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class SettingsTabBase : MonoBehaviour, INeedDependencyInjection, IInitializable
    {
        public virtual bool hasChanged => _baseSettings.hasChanged;

        [SerializeField] protected BaseSettings _baseSettings = new BaseSettings();

        protected float _fadeAnimationDuration => _baseSettings.uiConfigs != null ? _baseSettings.uiConfigs.uiFadeAnimationDuration : UIConfigs.defaultUIFadeAnimationDuration;
        protected Ease _fadeEase => _baseSettings.uiConfigs != null ? _baseSettings.uiConfigs.uiFadeEase : UIConfigs.defaultUIFadeEase;

        public virtual void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);
            _baseSettings.Initialize();

            _baseSettings.canvasGroup = GetComponent<CanvasGroup>();
        }

        public async void Open()
        {
            await OpenAsync();
        }

        public async void Close()
        {
            await CloseAsync();
        }

        public virtual async Task OpenAsync()
        {
            float duration = _fadeAnimationDuration;

            if (_baseSettings.isOpen == true) duration = 0;

            gameObject.SetActive(true);
            _baseSettings.canvasGroup?.FadeUp(duration, ease: _fadeEase);

            await AsyncHelper.DelayFloat(duration);

            _baseSettings.isOpen = true;
        }

        public virtual async Task CloseAsync()
        {
            float duration = _fadeAnimationDuration;

            if (_baseSettings.isOpen == false) duration = 0;

            _baseSettings.canvasGroup?.FadeDown(duration, ease: _fadeEase);

            await AsyncHelper.DelayFloat(duration);

            gameObject.SetActive(false);
            _baseSettings.isOpen = false;
        }

        public abstract Task ApplyAsync();
        public abstract Task RestoreAsync();

        [Serializable]
        protected class BaseSettings : IInitializable, INeedDependencyInjection
        {
            [Header("Components")]
            public CanvasGroup canvasGroup;

            [Header("DI")]
            [Inject] public UIConfigs uiConfigs;

            [Header("Debug")]
            public bool isOpen = false;
            public bool hasChanged = false;

            public void Initialize()
            {
                DependencyContext.diBox.InjectDataTo(this);
            }
        }
    }
}