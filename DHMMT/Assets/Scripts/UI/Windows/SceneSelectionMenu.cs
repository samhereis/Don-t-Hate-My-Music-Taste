using ConstStrings;
using DataClasses;
using DI;
using Events;
using Helpers;
using Interfaces;
using SO.Lists;
using System;
using TMPro;
using SamhereisTools;
using UI.Canvases;
using UI.Elements.SceneSelectMenu;
using UI.Interaction;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Windows
{
    public class SceneSelectionMenu : CanvasWindowBase, IDIDependent
    {
        [Header("Components")]
        [SerializeField] private Transform _scenesUnitsParent;
        [SerializeField] private BackButton _backButton;

        [Header("Windows")]
        [SerializeField] private MainMenu _mainMenu;

        [Header("Prefabs")]
        [SerializeField] private SceneUnit _sceneUnitPrefab;

        [Header("Addressables")]
        [SerializeField] private AssetReferenceGameObject _loadingScreen;

        [Header("DI")]
        [DI(DIStrings.listOfAllScenes)][SerializeField] private ListOfAllScenes _listOfAllScenes;
        [DI(DIStrings.sceneLoader)][SerializeField] private SceneLoader _sceneLoader;
        [DI(Event_DIStrings.onASceneSelected)][SerializeField] private EventWithOneParameters<AScene_Extended> _onASceneSelected;
        [DI(Event_DIStrings.onASceneLoadRequested)][SerializeField] private EventWithOneParameters<AScene_Extended> _onASceneLoadRequested;

        [Header("Blocks")]
        [SerializeField] private PlayButtonBlock _playButtonBlock = new PlayButtonBlock();
        [SerializeField] private DescriptionBlock _descriptionBlock = new DescriptionBlock();
        [SerializeField] private RulesBlock _rulesBlock = new RulesBlock();

        public override void Enable(float? duration = null)
        {
            FillSceneUnits();

            base.Enable(duration);

            SubscribeToEvents();

            _playButtonBlock.Initialize();
            _descriptionBlock.Initialize();
            _rulesBlock.Initialize();

            _onASceneSelected?.Invoke(_listOfAllScenes.GetScenes()[0]);
        }

        public override void Disable(float? duration = null)
        {
            UnsubscribeFromEvents();

            base.Disable(duration);

            _playButtonBlock.Clear();
            _descriptionBlock.Clear();
            _rulesBlock.Clear();

            Clear();
        }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            _backButton?.SubscribeToEvents();
            _backButton?.onBack.AddListener(OnBackButtonClicked);

            _onASceneSelected.AddListener(SelectScene);
            _onASceneLoadRequested.AddListener(OpenScene);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            _backButton?.UnsubscribeFromEvents();
            _backButton?.onBack.RemoveListener(OnBackButtonClicked);

            _onASceneSelected.RemoveListener(SelectScene);
            _onASceneLoadRequested.RemoveListener(OpenScene);
        }

        private void OnBackButtonClicked()
        {
            _mainMenu?.Enable();
        }

        private void SelectScene(AScene_Extended scene)
        {
            _playButtonBlock.UpdateView(scene);
            _descriptionBlock.UpdateView(scene);
            _rulesBlock.UpdateView(scene);
        }

        private async void OpenScene(AScene_Extended scene)
        {
            if (_sceneLoader == null)
            {
                SceneManager.LoadSceneAsync(scene.sceneCode);
            }
            else
            {
                SceneLoadingMenu loadingScreenInstance = await AddressablesHelper.InstantiateAsync<SceneLoadingMenu>(_loadingScreen);

                if (loadingScreenInstance != null)
                {
                    loadingScreenInstance.SetProgress(0f);

                    Disable();
                    loadingScreenInstance.Disable();

                    DontDestroyOnLoad(loadingScreenInstance);

                    await AsyncHelper.DelayFloat(1f);

                    await _sceneLoader.LoadSceneAsync(scene, (percent) =>
                    {
                        loadingScreenInstance.SetProgress(percent);
                    });

                    Addressables.ReleaseInstance(loadingScreenInstance.gameObject);
                }
                else
                {
                    SceneManager.LoadSceneAsync(scene.sceneCode);
                }
            }
        }

        private void FillSceneUnits()
        {
            Clear();

            foreach (var scene in _listOfAllScenes.GetScenes())
            {
                var sceneUnit = Instantiate(_sceneUnitPrefab, _scenesUnitsParent);
                sceneUnit.Initialize(scene);
            }
        }

        private void Clear()
        {
            foreach (var sceneUnit in _scenesUnitsParent.GetComponentsInChildren<SceneUnit>(true))
            {
                Destroy(sceneUnit.gameObject);
            }
        }

        [Serializable]
        public class PlayButtonBlock : IInitializable, IClearable, IDIDependent
        {
            [DI(Event_DIStrings.onASceneLoadRequested)][SerializeField] private EventWithOneParameters<AScene_Extended> _onASceneLoadRequested;

            [SerializeField] private CanvasGroup _canvasGroup;
            [SerializeField] private Image _sceneIcon;
            [SerializeField] private Button _playButton;
            [SerializeField] private TextMeshProUGUI _sceneName;

            [Header("Settings")]
            [SerializeField] private float _downFadeDuration = 0.25f;
            [SerializeField] private float _upFadeDuration = 0.5f;

            [SerializeField] private AScene_Extended _scene;

            public void Initialize()
            {
                (this as IDIDependent)?.LoadDependencies();

                _playButton.onClick.AddListener(OnPlayClicked);
            }

            public void UpdateView(AScene_Extended scene)
            {
                _scene = scene;
                _canvasGroup.FadeDown(_downFadeDuration, completeCallback: () =>
                {
                    _sceneIcon.sprite = _scene.GetIcon();
                    _sceneName.text = _scene.GetSceneName();

                    _canvasGroup.FadeUp(_upFadeDuration);
                });
            }

            public void Clear()
            {
                _playButton.onClick.RemoveListener(OnPlayClicked);
            }

            private void OnPlayClicked()
            {
                _onASceneLoadRequested?.Invoke(_scene);
            }
        }

        [Serializable]
        public class DescriptionBlock : IInitializable, IClearable, IDIDependent
        {
            [SerializeField] private CanvasGroup _canvasGroup;
            [SerializeField] private TextMeshProUGUI _descriptionText;

            [Header("Settings")]
            [SerializeField] private float _downFadeDuration = 0.25f;
            [SerializeField] private float _upFadeDuration = 0.5f;

            [SerializeField] private AScene_Extended _scene;

            public void Initialize()
            {
                (this as IDIDependent)?.LoadDependencies();
            }

            public void UpdateView(AScene_Extended scene)
            {
                _canvasGroup.FadeDown(_downFadeDuration, completeCallback: () =>
                {
                    _descriptionText.text = scene.GetDescription();

                    _canvasGroup.FadeUp(_upFadeDuration);
                });
            }

            public void Clear()
            {

            }
        }

        [Serializable]
        public class RulesBlock : IInitializable, IClearable, IDIDependent
        {
            [SerializeField] private CanvasGroup _canvasGroup;
            [SerializeField] private TextMeshProUGUI _rulesText;

            [Header("Settings")]
            [SerializeField] private float _downFadeDuration = 0.25f;
            [SerializeField] private float _upFadeDuration = 0.5f;

            [SerializeField] private AScene_Extended _scene;

            public void Initialize()
            {
                (this as IDIDependent)?.LoadDependencies();
            }

            public void UpdateView(AScene_Extended scene)
            {
                _canvasGroup.FadeDown(_downFadeDuration, completeCallback: () =>
                {
                    _rulesText.text = scene.GetRules();

                    _canvasGroup.FadeUp(_upFadeDuration);
                });
            }

            public void Clear()
            {

            }
        }
    }
}