using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Canvases;
using UI.Elements;
using UI.Elements.SettingsTab;
using UI.Interaction;
using UnityEngine;

namespace UI.Windows
{
    public class SettingsMenu : CanvasWindowBase
    {
        [Header("Menus")]
        [SerializeField] private MainMenu _mainMenu;

        [Header("UI Elements")]
        [SerializeField] private CanvasGroup _activeTabIndicator;
        [SerializeField] private Transform _activeTabIndicatorParent;

        [Header("Buttons")]
        [SerializeField] private BackButton _backButton;

        [Header("Tabs")]
        [SerializeField] private SettingsTabBase _gameplayTab;
        [SerializeField] private SettingsTabBase _audioTab;
        [SerializeField] private SettingsTabBase _graphicsTab;
        [SerializeField] private List<SettingsTabBase> _allSettingsTabs;

        [Header("Tab buttons")]
        [SerializeField] private SettingsTabButton _gameplayTabButton;
        [SerializeField] private SettingsTabButton _audioTabButton;
        [SerializeField] private SettingsTabButton _graphicsTabButton;

        private void OnValidate()
        {
            _activeTabIndicatorParent = _activeTabIndicator.transform.parent;
        }

        public override void Enable(float? duration = null)
        {
            _allSettingsTabs = new List<SettingsTabBase> { _gameplayTab, _audioTab, _graphicsTab };

            foreach (var settingsTab in _allSettingsTabs)
            {
                settingsTab.Initialize();
            }

            base.Enable(duration);
            OnGameplayTabButtonClicked();

            SubscribeToEvents();
        }

        public override void Disable(float? duration = null)
        {
            UnsubscribeFromEvents();

            CloseAllSettingTabs();
            base.Disable(duration);
        }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            _gameplayTabButton.onClick += OnGameplayTabButtonClicked;
            _audioTabButton.onClick += OnAudioTabButtonClicked;
            _graphicsTabButton.onClick += OnGraphicsTabButtonClicked;

            _backButton?.SubscribeToEvents();
            _backButton?.onBack.AddListener(Exit);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            _gameplayTabButton.onClick -= OnGameplayTabButtonClicked;
            _audioTabButton.onClick -= OnAudioTabButtonClicked;
            _graphicsTabButton.onClick -= OnGraphicsTabButtonClicked;

            _backButton?.UnsubscribeFromEvents();
            _backButton?.onBack.RemoveListener(Exit);
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
            CloseAllSettingTabs(settingsTab);
            await settingsTab.OpenAsync();
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
    }
}