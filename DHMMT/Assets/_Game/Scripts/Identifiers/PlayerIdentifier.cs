using ConstStrings;
using DataClasses;
using Demo.Scripts.Runtime.Base;
using DependencyInjection;
using Interfaces;
using Observables;
using Settings;
using UnityEngine;

namespace Identifiers
{
    public class PlayerIdentifier : IdentifierBase, IDamagerActor, INeedDependencyInjection, ISubscribesToEvents
    {
        [Header("Components")]
        [SerializeField] private FPSController _fpsController;
        [SerializeField] private Camera _camera;

        [Header("DI")]
        [Inject(ObservableValue_ConstStrings.isPlayerAiming)] private ObservableValue<bool> _isPlayerAiming;
        [Inject(ObservableValue_ConstStrings.playerWeaponData)] private ObservableValue<PlayerWeaponData> _playerWeaponData;
        [Inject(Savables_ConstStrings.fieldOfView_Settings)][SerializeField] private FloatSavable_SO _fieldOfView_Settings;

        [field: SerializeField] public IdentifierBase damagerIdentifier { get; private set; }

        private void Awake()
        {
            damagerIdentifier = this;
            if (_camera == null) { _camera = GetComponentInChildren<Camera>(true); }
        }

        private void Start()
        {
            DependencyContext.diBox.InjectDataTo(this);

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

        public void OnHasDamaged(ADamage aDamage)
        {
            throw new System.NotImplementedException();
        }
    }
}