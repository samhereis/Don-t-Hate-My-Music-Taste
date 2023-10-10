using ConstStrings;
using DI;
using Managers;
using SamhereisTools;
using SO.Lists;
using UI.Canvases;
using UI.Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class PauseMenu : CanvasWindowBase
    {
        [Header("UI Elements")]
        [SerializeField] private Button _resumeButton;
        [SerializeField] private BackButton _backButton;
        [SerializeField] private Button _mainMenuButton;

        [Header(HeaderStrings.Components)]
        [SerializeField] private GameplayMenu _gameplayMenu;

        [Header("DI")]
        [DI(DIStrings.sceneLoader)][SerializeField] private SceneLoader _sceneLoader;
        [DI(DIStrings.listOfAllScenes)][SerializeField] private ListOfAllScenes _listOfAllScenes;

        protected override void Awake()
        {
            base.Awake();

            if (_gameplayMenu == null) { _gameplayMenu = FindFirstObjectByType<GameplayMenu>(FindObjectsInactive.Include); }
        }

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

        public async void GoToMainMenu()
        {
            await _sceneLoader.LoadSceneAsync(_listOfAllScenes.mainMenu);
        }

        public void Resume()
        {
            _gameplayMenu?.Enable();
        }
    }
}