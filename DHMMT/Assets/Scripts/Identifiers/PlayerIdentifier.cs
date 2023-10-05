using ConstStrings;
using DataClasses;
using Demo.Scripts.Runtime.Base;
using DI;
using Interfaces;
using UnityEngine;
using Values;

namespace Identifiers
{
    public class PlayerIdentifier : IdentifierBase, IDamagerActor, IDIDependent, ISubscribesToEvents
    {
        [SerializeField] private FPSController _fpsController;

        [Header("DI")]
        [DI(Event_DIStrings.isPlayerAiming)][SerializeField] private ValueEvent<bool> _isPlayerAiming;
        [DI(Event_DIStrings.playerWeaponData)][SerializeField] private ValueEvent<PlayerWeaponData> _playerWeaponData;

        [field: SerializeField] public IdentifierBase damagerIdentifier { get; private set; }

        private void Awake()
        {
            damagerIdentifier = this;
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();

            _fpsController.Initialize();
            SubscribeToEvents();
        }

        public void SubscribeToEvents()
        {
            _fpsController.isPlayerAimingChangedEvent += OnPlayerAimingChanged;

            _fpsController.onChangeWeapon += OnPlayerWeaponDataChanged;
            _fpsController.onPlayerShoot += OnPlayerWeaponDataChanged;
            _fpsController.onPlayerReloaded += OnPlayerWeaponDataChanged;
        }

        public void UnsubscribeFromEvents()
        {
            _fpsController.isPlayerAimingChangedEvent -= OnPlayerAimingChanged;

            _fpsController.onChangeWeapon -= OnPlayerWeaponDataChanged;
            _fpsController.onPlayerShoot -= OnPlayerWeaponDataChanged;
            _fpsController.onPlayerReloaded -= OnPlayerWeaponDataChanged;
        }

        private void OnPlayerAimingChanged(bool isAiming)
        {
            _isPlayerAiming.ChangeValue(isAiming);
        }

        private void OnPlayerWeaponDataChanged(WeaponIdentifier weaponIdentifier)
        {
            var playerWeaponData = new PlayerWeaponData(weaponIdentifier.maxAmmo, weaponIdentifier.currentAmmo, weaponIdentifier);

            _playerWeaponData?.ChangeValue(playerWeaponData);
        }

        public void OnDamaged(ADamage aDamage)
        {

        }
    }
}