using ConstStrings;
using DependencyInjection;
using Identifiers;
using Interfaces;
using Managers;
using Observables;
using Services;
using System;
using UI.Canvases;
using UI.Elements.GameplayTab;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Windows
{
    public class GameplayMenu : MenuBase, INeedDependencyInjection
    {
        public Action onPauseRequested;

        [Header("Elements")]
        [SerializeField] private CrosshairIdentifier _crosshairIdentifier;
        [field: SerializeField] public KillsCountDisplayer killsCountDisplayer { get; private set; }

        [Header("DI")]
        [Inject(ObservableValue_ConstStrings.isPlayerAiming)][SerializeField] private ObservableValue<bool> _isPlayerAiming;
        [Inject] private PlayerInputService _input_SO;

        protected override void Awake()
        {
            base.Awake();

            DependencyContext.diBox.InjectDataTo(this);

            _crosshairIdentifier = GetComponentInChildren<CrosshairIdentifier>(true);
            killsCountDisplayer = GetComponentInChildren<KillsCountDisplayer>(true);
        }

        public override void Enable(float? duration = null)
        {
            GlobalGameSettings.EnambleGameplayMode();

            base.Enable(duration);

            SubscribeToEvents();

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

            _isPlayerAiming.AddListener(OnPlayerAimingChanged);
            _input_SO.input.Gameplay.Pause.performed += Pause;

            onSubscribeToEvents?.Invoke();
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            _isPlayerAiming.RemoveListener(OnPlayerAimingChanged);
            _input_SO.input.Gameplay.Pause.performed -= Pause;

            onUnsubscribeFromEvents?.Invoke();
        }

        private void Pause(InputAction.CallbackContext context)
        {
            onPauseRequested?.Invoke();
        }

        private void OnPlayerAimingChanged(bool isAiming)
        {
            if (isAiming)
            {
                _crosshairIdentifier?.Hide();
            }
            else
            {
                _crosshairIdentifier?.Show();
            }
        }

        public void IncreaseKillsCount(IDamagable damagable)
        {
            killsCountDisplayer.IncreaseKillsCount();
        }
    }
}