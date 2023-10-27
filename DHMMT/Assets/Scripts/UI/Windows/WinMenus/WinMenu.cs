using ConstStrings;
using DI;
using Managers;
using SamhereisTools;
using SO.Lists;
using System;
using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class WinMenu : CanvasWindowBase
    {
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

            _mainMenuButton.onClick.AddListener(GoToMainMenu);
            _replayButton.onClick.AddListener(Replay);

            onUnsubscribeFromEvents?.Invoke();
        }

        public virtual void OnWin()
        {
            Enable();
        }

        public async void GoToMainMenu()
        {

        }

        public async void Replay()
        {

        }
    }
}