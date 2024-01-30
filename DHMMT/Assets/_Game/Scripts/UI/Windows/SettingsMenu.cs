using Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Canvases;
using UI.Elements;
using UI.Elements.SettingsTab;
using UI.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class SettingsMenu : MenuBase
    {
        [Header("UI Elements")]
        [SerializeField] private CanvasGroup _activeTabIndicator;
        [SerializeField] private Transform _activeTabIndicatorParent;
        [SerializeField] private CanvasGroup _onChangedButtons;

        [Header("Buttons")]
        [SerializeField] private BackButton _backButton;
        [SerializeField] private Button _restoreButton;
        [SerializeField] private Button _applyButton;

        [Header("Tabs")]
        [SerializeField] private SettingsTabBase _gameplayTab;
        [SerializeField] private SettingsTabBase _audioTab;
        [SerializeField] private SettingsTabBase _graphicsTab;

        [Header("Tab buttons")]
        [SerializeField] private SettingsTabButton _gameplayTabButton;
        [SerializeField] private SettingsTabButton _audioTabButton;
        [SerializeField] private SettingsTabButton _graphicsTabButton;

        private List<SettingsTabBase> _allSettingsTabs = new List<SettingsTabBase>();

        private SettingsTabBase _currentSettingsTab;

        private MenuBase openOnBack;

        private void OnValidate()
        {
            _activeTabIndicatorParent = _activeTabIndicator.transform.parent;
        }

        public void Construct(MenuBase openOnBack)
        {
            this.openOnBack = openOnBack;

            _allSettingsTabs = new List<SettingsTabBase> { _gameplayTab, _audioTab, _graphicsTab };

            foreach (var settingsTab in _allSettingsTabs)
            {
                settingsTab.Initialize();
            }
        }

        public override void Enable(float? duration = null)
        {
            base.Enable(duration);

            foreach (var settingsTab in _allSettingsTabs)
            {
                settingsTab.Initialize();
            }

            OnGameplayTabButtonClicked();

            SubscribeToEvents();

            StartCoroutine(CheckOnChanged());
        }

        public override void Disable(float? duration = null)
        {
            UnsubscribeFromEvents();

            CloseAllSettingTabs();
            base.Disable(duration);

            StopCoroutine(CheckOnChanged());
        }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            _gameplayTabButton.onClick += OnGameplayTabButtonClicked;
            _audioTabButton.onClick += OnAudioTabButtonClicked;
            _graphicsTabButton.onClick += OnGraphicsTabButtonClicked;

            _backButton?.SubscribeToEvents();
            _backButton?.onBack.AddListener(Exit);
            _restoreButton?.onClick.AddListener(RestoreCurrentSettingsTab);
            _applyButton?.onClick.AddListener(ApplyCurrentSettingsTab);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            _gameplayTabButton.onClick -= OnGameplayTabButtonClicked;
            _audioTabButton.onClick -= OnAudioTabButtonClicked;
            _graphicsTabButton.onClick -= OnGraphicsTabButtonClicked;

            _backButton?.UnsubscribeFromEvents();
            _backButton?.onBack.RemoveListener(Exit);
            _restoreButton?.onClick.RemoveListener(RestoreCurrentSettingsTab);
            _applyButton?.onClick.RemoveListener(ApplyCurrentSettingsTab);
        }

        private void Exit()
        {
            openOnBack?.Enable();
        }

        public async void OnGameplayTabButtonClicked()
        {
            _gameplayTabButton.SetActiveTabIndicator(_activeTabIndicator, _activeTabIndicatorParent);
            await OpenSettingsTabAsync(_gameplayTab);
        }

        public async void OnAudioTabButtonClicked()
        {
            _audioTabButton.SetActiveTabIndicator(_activeTabIndicator, _activeTabIndicatorParent);
            await OpenSettingsTabAsync(_audioTab);
        }

        public async void OnGraphicsTabButtonClicked()
        {
            _graphicsTabButton.SetActiveTabIndicator(_activeTabIndicator, _activeTabIndicatorParent);
            await OpenSettingsTabAsync(_graphicsTab);
        }

        private async Task OpenSettingsTabAsync(SettingsTabBase settingsTab)
        {
            RestoreCurrentSettingsTab();

            _currentSettingsTab = settingsTab;

            CloseAllSettingTabs(_currentSettingsTab);
            await _currentSettingsTab.OpenAsync();
        }

        private void CloseAllSettingTabs(SettingsTabBase except = null)
        {
            foreach (var settingsTab in _allSettingsTabs)
            {
                if (settingsTab != except)
                {
                    CloseAsync(settingsTab);
                }
            }

            async void CloseAsync(SettingsTabBase settingsTab)
            {
                await settingsTab.CloseAsync();
            }
        }

        private IEnumerator CheckOnChanged()
        {
            float duration = 0.25f;

            while (gameObject.activeSelf)
            {
                if (_currentSettingsTab.hasChanged)
                {
                    _onChangedButtons.FadeUp(duration);
                }
                else
                {
                    _onChangedButtons.FadeDown(duration);
                }

                yield return new WaitForSecondsRealtime(duration);
            }
        }

        private void RestoreCurrentSettingsTab()
        {
            _currentSettingsTab?.RestoreAsync();
        }

        private void ApplyCurrentSettingsTab()
        {
            _currentSettingsTab?.ApplyAsync();
        }
    }
}