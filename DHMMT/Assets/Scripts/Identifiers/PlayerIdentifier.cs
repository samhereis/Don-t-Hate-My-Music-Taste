using ConstStrings;
using DataClasses;
using Demo.Scripts.Runtime.Base;
using DI;
using Interfaces;
using Settings;
using UnityEngine;
using Values;

namespace Identifiers
{
    public class PlayerIdentifier : IdentifierBase, IDamagerActor, IDIDependent, ISubscribesToEvents
    {
        [Header(HeaderStrings.Components)]
        [SerializeField] private FPSController _fpsController;
        [SerializeField] private Camera _camera;

        [Header("DI")]
        [DI(Event_DIStrings.isPlayerAiming)][SerializeField] private ValueEvent<bool> _isPlayerAiming;
        [DI(Event_DIStrings.playerWeaponData)][SerializeField] private ValueEvent<PlayerWeaponData> _playerWeaponData;
        [DI(DIStrings.fieldOfView_Settings)][SerializeField] private FloatSavable_SO _fieldOfView_Settings;

        [field: SerializeField] public IdentifierBase damagerIdentifier { get; private set; }

        private void Awake()
        {
            damagerIdentifier = this;
            if (_camera == null) { _camera = GetComponentInChildren<Camera>(true); }
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();
            if (_camera != null && _fieldOfView_Settings != null) { _camera.fieldOfView = _fieldOfView_Settings.currentValue; }
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