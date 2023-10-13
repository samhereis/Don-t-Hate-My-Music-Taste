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
    public class LoseMenu : CanvasWindowBase
    {
        public Action onGoToMainMenuRequest;

        [Header("UI Elements")]
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _replayButton;

        [Header("DI")]
        [DI(DIStrings.sceneLoader)][SerializeField] private SceneLoader _sceneLoader;
        [DI(DIStrings.listOfAllScenes)][SerializeField] private ListOfAllScenes _listOfAllScenes;

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

        public async void GoToMainMenu()
        {
            onGoToMainMenuRequest?.Invoke();
        }

        public async void Replay()
        {
            await _sceneLoader.LoadLastScene();
        }
    }
}