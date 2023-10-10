using ConstStrings;
using DI;
using Identifiers;
using Interfaces;
using Managers;
using PlayerInputHolder;
using UI.Canvases;
using UI.Elements.GameplayTab;
using UnityEngine;
using UnityEngine.InputSystem;
using Values;

namespace UI.Windows
{
    public class GameplayMenu : CanvasWindowBase, IDIDependent
    {
        [Header(HeaderStrings.Components)]
        [SerializeField] private PauseMenu _pauseMenu;

        [Header("Elements")]
        [SerializeField] private CrosshairIdentifier _crosshairIdentifier;
        [field: SerializeField] public KillsCountDisplayer killsCountDisplayer { get; private set; }

        [Header("DI")]
        [DI(Event_DIStrings.isPlayerAiming)][SerializeField] private ValueEvent<bool> _isPlayerAiming;
        [DI(DIStrings.inputHolder)][SerializeField] private Input_SO _input_SO;

        protected override void Awake()
        {
            base.Awake();
            (this as IDIDependent).LoadDependencies();

            _crosshairIdentifier = GetComponentInChildren<CrosshairIdentifier>(true);
            killsCountDisplayer = GetComponentInChildren<KillsCountDisplayer>(true);

            if (_pauseMenu == null) { _pauseMenu = FindFirstObjectByType<PauseMenu>(FindObjectsInactive.Include); }
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
            _pauseMenu.Enable();
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