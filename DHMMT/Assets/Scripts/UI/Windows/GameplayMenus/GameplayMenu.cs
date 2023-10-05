using ConstStrings;
using DI;
using Events;
using Identifier;
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
        [DI(Event_DIStrings.onEnemyDied)][SerializeField] private EventWithOneParameters<IDamagable> _onEnemyDied;

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
            _onEnemyDied.AddListener(OnEnemyDied);

            onSubcsribeToEvents?.Invoke();
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            _isPlayerAiming.RemoveListener(OnPlayerAimingChanged); 
            _onEnemyDied.RemoveListener(OnEnemyDied);

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

        private void OnEnemyDied(IDamagable damagable)
        {
            killsCountDisplayer.IncreaseKillsCount();
        }
    }
}