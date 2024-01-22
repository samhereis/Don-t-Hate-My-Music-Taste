using Managers;
using System;
using UI.Canvases;
using UI.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class PauseMenu : MenuBase
    {
        public Action onResumeRequest;
        public Action onGoToMainMenuRequest;

        [Header("UI Elements")]
        [SerializeField] private Button _resumeButton;
        [SerializeField] private BackButton _backButton;
        [SerializeField] private Button _mainMenuButton;

        public override void Enable(float? duration = null)
        {
            base.Enable(duration);

            SubscribeToEvents();

            GlobalGameSettings.EnableUIMode();

            onEnable?.Invoke();
        }

        public override void Disable(float? duration = null)
        {
            UnsubscribeFromEvents();

            base.Disable(duration);

            onDisable?.Invoke();
        }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            _resumeButton.onClick.AddListener(Resume);
            _backButton.onBack.AddListener(Resume);
            _mainMenuButton.onClick.AddListener(GoToMainMenu);

            _backButton.SubscribeToEvents();

            onSubscribeToEvents?.Invoke();
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            _resumeButton.onClick.RemoveListener(Resume);
            _backButton.onBack.RemoveListener(Resume);
            _mainMenuButton.onClick.RemoveListener(GoToMainMenu);

            _backButton.UnsubscribeFromEvents();

            onUnsubscribeFromEvents?.Invoke();
        }

        public void GoToMainMenu()
        {
            onGoToMainMenuRequest?.Invoke();
        }

        public void Resume()
        {
            onResumeRequest?.Invoke();
        }
    }
}