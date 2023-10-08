using ConstStrings;
using DI;
using Identifiers;
using Interfaces;
using Managers;
using System;
using UI.Canvases;
using UI.Elements.GameplayTab;
using UnityEngine;
using Values;

namespace UI.Windows
{
    public class GameplayMenu : CanvasWindowBase, IDIDependent
    {
        public Action onOpen;
        public Action onClose;

        public Action onSubcsribeToEvents;
        public Action onUnsubscribeFromEvents;

        [Header("Elements")]
        [SerializeField] private CrosshairIdentifier _crosshairIdentifier;
        [field: SerializeField] public KillsCountDisplayer killsCountDisplayer { get; private set; }

        [Header("DI")]
        [DI(Event_DIStrings.isPlayerAiming)][SerializeField] private ValueEvent<bool> _isPlayerAiming;

        protected override void Awake()
        {
            base.Awake();
            (this as IDIDependent).LoadDependencies();

            _crosshairIdentifier = GetComponentInChildren<CrosshairIdentifier>(true);
            killsCountDisplayer = GetComponentInChildren<KillsCountDisplayer>(true);
        }

        public override void Enable(float? duration = null)
        {
            GlobalGameSettings.EnambleGameplayMode();

            base.Enable(duration);

            SubscribeToEvents();

            onOpen?.Invoke();
        }

        public override void Disable(float? duration = null)
        {
            UnsubscribeFromEvents();

            base.Disable(duration);

            onClose?.Invoke();
        }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            _isPlayerAiming.AddListener(OnPlayerAimingChanged);

            onSubcsribeToEvents?.Invoke();
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            _isPlayerAiming.RemoveListener(OnPlayerAimingChanged);

            onUnsubscribeFromEvents?.Invoke();
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