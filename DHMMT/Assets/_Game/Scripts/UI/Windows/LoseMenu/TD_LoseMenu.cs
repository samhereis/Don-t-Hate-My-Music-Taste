using DependencyInjection;
using Services;
using SO.Lists;
using System;
using TMPro;
using UI.Canvases;
using UI.Windows.GameplayMenus;
using UnityEngine;

namespace UI.Windows
{
    public class TD_LoseMenu : MenuExtenderBase<LoseMenu>, INeedDependencyInjection
    {
        [Header("Windows")]
        [SerializeField] private TD_GameplayMenu _tD_GameplayMenu;

        [Header("UIElements")]
        [SerializeField] private TextMeshProUGUI _bestScoreText;
        [SerializeField] private TextMeshProUGUI _currentScoreText;

        [Header("DI")]
        [Inject] private GameSaveService _gameSaveManager;
        [Inject] private ListOfAllScenes_Extended _listOfAllScenes;

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
            DependencyContext.diBox.InjectDataTo(this);

            _currentScoreText.text = TimeSpan.FromSeconds(_tD_GameplayMenu.currentDuration).ToString();

            var recordScore = 0;
            var saveUnit = _gameSaveManager.modeTDSaves.tD_Saves.Find(x => x.sceneName == _listOfAllScenes.lastLoadedScene.sceneCode);
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