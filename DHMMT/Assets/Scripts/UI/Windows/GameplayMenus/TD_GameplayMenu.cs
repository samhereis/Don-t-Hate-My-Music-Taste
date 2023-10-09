using ConstStrings;
using DI;
using Helpers;
using System;
using TMPro;
using UnityEngine;

namespace UI.Windows.GameplayMenus
{
    public class TD_GameplayMenu : MonoBehaviour, IDIDependent
    {
        public Action onTimerOver;

        [Header(HeaderStrings.Components)]
        [field: SerializeField] public GameplayMenu gameplayMenu;

        [Header(HeaderStrings.UIElements)]
        [SerializeField] private TextMeshProUGUI _timerText;

        [Header(HeaderStrings.Settings)]
        [SerializeField] private int _secondsOnStart;

        [Header(HeaderStrings.Debug)]
        [SerializeField] private bool _isTimerActive = false;
        [SerializeField] private int _currentTimer = 0;

        private void Awake()
        {
            if (gameplayMenu == null) { gameplayMenu = GetComponent<GameplayMenu>(); }

            gameplayMenu.onOpen += OnGameplayMenuOpen;
            gameplayMenu.onClose += OnGameplayMenuClose;
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();

            AddSeconds(_secondsOnStart);
            CalculateTimer();
        }

        private void OnDestroy()
        {
            gameplayMenu.onOpen -= OnGameplayMenuOpen;
            gameplayMenu.onClose -= OnGameplayMenuClose;
        }

        private void OnGameplayMenuOpen()
        {
            SetActiveStatusForTimer(true);
        }

        private void OnGameplayMenuClose()
        {
            SetActiveStatusForTimer(false);
        }

        public void SetActiveStatusForTimer(bool targetActiveStatus)
        {
            _isTimerActive = targetActiveStatus;
        }

        public void AddSeconds(int seconds)
        {
            _currentTimer += seconds;
        }

        private async void CalculateTimer()
        {
            UpdateTimerText();

            while (destroyCancellationToken.IsCancellationRequested == false)
            {
                await AsyncHelper.DelayFloat(1f);

                if (_isTimerActive && _currentTimer > 0)
                {
                    _currentTimer--;
                    UpdateTimerText();
                }

                if (_currentTimer <= 0)
                {
                    SetActiveStatusForTimer(false);
                    onTimerOver?.Invoke();
                }
            }
        }

        private void UpdateTimerText()
        {
            _timerText.text = _currentTimer.ToString();
        }
    }
}