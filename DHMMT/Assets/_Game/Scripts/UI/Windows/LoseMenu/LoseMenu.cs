using Managers;
using System;
using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class LoseMenu : MenuBase
    {
        public Action onGoToMainMenuRequest;
        public Action onReplayRequest;

        [Header("UI Elements")]
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _replayButton;

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

            _mainMenuButton.onClick.AddListener(GoToMainMenu);
            _replayButton.onClick.AddListener(Replay);

            onSubscribeToEvents?.Invoke();
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            _mainMenuButton.onClick.RemoveListener(GoToMainMenu);
            _replayButton.onClick.RemoveListener(Replay);

            onUnsubscribeFromEvents?.Invoke();
        }

        public virtual void OnWin()
        {
            Enable();
        }

        public void GoToMainMenu()
        {
            onGoToMainMenuRequest?.Invoke();
        }

        public void Replay()
        {
            onReplayRequest?.Invoke();
        }
    }
}