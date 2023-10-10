using ConstStrings;
using DI;
using Managers;
using SamhereisTools;
using System;
using TMPro;
using UI.Windows.GameplayMenus;
using UnityEngine;

namespace UI.Windows
{
    public class TD_LoseMenu : MonoBehaviour, IDIDependent
    {
        [Header("Windows")]
        [SerializeField] private LoseMenu _loseMenu;
        [SerializeField] private TD_GameplayMenu _tD_GameplayMenu;

        [Header(HeaderStrings.UIElements)]
        [SerializeField] private TextMeshProUGUI _bestScoreText;
        [SerializeField] private TextMeshProUGUI _currentScoreText;

        [Header(HeaderStrings.DI)]
        [DI(DIStrings.gameSaveManager)][SerializeField] private GameSaveManager _gameSaveManager;
        [DI(DIStrings.sceneLoader)][SerializeField] private SceneLoader _sceneLoader;

        private void Awake()
        {
            _loseMenu = GetComponent<LoseMenu>();

            _loseMenu.onEnable += OnOpen;
            _loseMenu.onDisable += OnClose;
            _loseMenu.onSubscribeToEvents += OnSubscribeToEvents;
            _loseMenu.onUnsubscribeFromEvents += OnUnsubscribeFromEvents;

            if (_tD_GameplayMenu == null) { _tD_GameplayMenu = FindFirstObjectByType<TD_GameplayMenu>(FindObjectsInactive.Include); }
        }

        private void OnDestroy()
        {
            _loseMenu.onEnable -= OnOpen;
            _loseMenu.onDisable -= OnClose;
            _loseMenu.onSubscribeToEvents -= OnSubscribeToEvents;
            _loseMenu.onUnsubscribeFromEvents -= OnUnsubscribeFromEvents;
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