using ConstStrings;
using DI;
using Helpers;
using System;
using TMPro;
using UI.Canvases;
using UnityEngine;

namespace UI.Windows.GameplayMenus
{
    public class TD_GameplayMenu : CanvasWindowExtendorBase<GameplayMenu>, IDIDependent
    {
        public Action onTimerOver;

        public int currentTimer => _currentTimer;
        public int currentDuration => _currentDuration;

        [Header(HeaderStrings.UIElements)]
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _durationText;

        [Header(HeaderStrings.Settings)]
        [SerializeField] private int _secondsOnStart;

        [Header(HeaderStrings.Debug)]
        [SerializeField] private bool _isTimerActive = false;
        [SerializeField] private int _currentTimer = 0;
        [SerializeField] private int _currentDuration = 0;

        private void Awake()
        {
            window.onEnable += OnGameplayMenuOpen;
            window.onDisable += OnGameplayMenuClose;
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();

            AddSeconds(_secondsOnStart);
            CalculateTimer();
        }

        private void OnDestroy()
        {
            window.onEnable -= OnGameplayMenuOpen;
            window.onDisable -= OnGameplayMenuClose;
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
                    _currentDuration++;
                    UpdateTimerText();

                    if (_currentTimer <= 0)
                    {
                        SetActiveStatusForTimer(false);
                        onTimerOver?.Invoke();
                    }
                }
            }
        }

        private void UpdateTimerText()
        {
            _timerText.text = TimeSpan.FromSeconds(_currentTimer).ToString();
            _durationText.text = TimeSpan.FromSeconds(_currentDuration).ToString();
        }
    }
}