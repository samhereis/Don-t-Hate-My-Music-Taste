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

        [field: SerializeField, Header(HeaderStrings.Components)] public GameplayMenu gameplayMenu { get; private set; }

        [Header(HeaderStrings.UIElements)]
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _durationText;

        [Header(HeaderStrings.Settings)]
        [SerializeField] private int _secondsOnStart;

        [Header(HeaderStrings.Debug)]
        [SerializeField] private bool _isTimerActive = false;
        [field: SerializeField] public int currentTimer { get; private set; } = 0;
        [field: SerializeField] public int currentDuration { get; private set; } = 0;

        private void Awake()
        {
            if (gameplayMenu == null) { gameplayMenu = GetComponent<GameplayMenu>(); }

            gameplayMenu.onEnable += OnGameplayMenuOpen;
            gameplayMenu.onDisable += OnGameplayMenuClose;
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();

            AddSeconds(_secondsOnStart);
            CalculateTimer();
        }

        private void OnDestroy()
        {
            gameplayMenu.onEnable -= OnGameplayMenuOpen;
            gameplayMenu.onDisable -= OnGameplayMenuClose;
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
            currentTimer += seconds;
        }

        private async void CalculateTimer()
        {
            UpdateTimerText();

            while (destroyCancellationToken.IsCancellationRequested == false)
            {
                await AsyncHelper.DelayFloat(1f);

                if (_isTimerActive && currentTimer > 0)
                {
                    currentTimer--;
                    currentDuration++;
                    UpdateTimerText();

                    if (currentTimer <= 0)
                    {
                        SetActiveStatusForTimer(false);
                        onTimerOver?.Invoke();
                    }
                }
            }
        }

        private void UpdateTimerText()
        {
            _timerText.text = TimeSpan.FromSeconds(currentTimer).ToString();
            _durationText.text = TimeSpan.FromSeconds(currentDuration).ToString();
        }
    }
}