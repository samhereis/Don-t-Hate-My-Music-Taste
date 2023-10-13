using ConstStrings;
using DI;
using Managers;
using SamhereisTools;
using System;
using TMPro;
using UI.Canvases;
using UI.Windows.GameplayMenus;
using UnityEngine;

namespace UI.Windows
{
    public class TD_LoseMenu : CanvasWindowExtendorBase<LoseMenu>, IDIDependent
    {
        [Header("Windows")]
        [SerializeField] private TD_GameplayMenu _tD_GameplayMenu;

        [Header(HeaderStrings.UIElements)]
        [SerializeField] private TextMeshProUGUI _bestScoreText;
        [SerializeField] private TextMeshProUGUI _currentScoreText;

        [Header(HeaderStrings.DI)]
        [DI(DIStrings.gameSaveManager)][SerializeField] private GameSaveManager _gameSaveManager;
        [DI(DIStrings.sceneLoader)][SerializeField] private SceneLoader _sceneLoader;

        private void Awake()
        {
            window.onEnable += OnOpen;
            window.onDisable += OnClose;
            window.onSubscribeToEvents += OnSubscribeToEvents;
            window.onUnsubscribeFromEvents += OnUnsubscribeFromEvents;

            if (_tD_GameplayMenu == null) { _tD_GameplayMenu = FindFirstObjectByType<TD_GameplayMenu>(FindObjectsInactive.Include); }
        }

        private void OnDestroy()
        {
            window.onEnable -= OnOpen;
            window.onDisable -= OnClose;
            window.onSubscribeToEvents -= OnSubscribeToEvents;
            window.onUnsubscribeFromEvents -= OnUnsubscribeFromEvents;
        }

        private void OnOpen()
        {
            (this as IDIDependent).LoadDependencies();

            _currentScoreText.text = TimeSpan.FromSeconds(_tD_GameplayMenu.currentDuration).ToString();

            var recordScore = 0;
            var saveUnit = _gameSaveManager.modeTDSaves.tD_Saves.Find(x => x.sceneName == _sceneLoader.lastLoadedScene.sceneCode);
            if (saveUnit != null) { recordScore = saveUnit.record; }
            _bestScoreText.text = TimeSpan.FromSeconds(recordScore).ToString();
        }

        private void OnClose()
        {

        }

        private void OnSubscribeToEvents()
        {

        }

        private void OnUnsubscribeFromEvents()
        {

        }
    }
}