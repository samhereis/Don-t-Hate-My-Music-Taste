using ConstStrings;
using DataClasses;
using Demo.Scripts.Runtime;
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
        [SerializeField] private FPSData _fpsData;

        [Header("DI")]
        [Inject(ObservableValue_ConstStrings.isPlayerAiming)] private ObservableValue<bool> _isPlayerAiming;
        [Inject(ObservableValue_ConstStrings.playerWeaponData)] private ObservableValue<PlayerWeaponData> _playerWeaponData;
        [Inject(Savables_ConstStrings.fieldOfView_Settings)][SerializeField] private FloatSavable_SO _fieldOfView_Settings;

        [field: SerializeField] public IdentifierBase damagerIdentifier { get; private set; }

        private void Awake()
        {
            damagerIdentifier = this;
            if (_camera == null) { _camera = GetComponentInChildren<Camera>(true); }
            if (_fpsData == null) { _fpsData = GetComponentInChildren<FPSData>(true); }

            DependencyContext.diBox.InjectDataTo(this);
            if (_camera != null && _fieldOfView_Settings != null) { _camera.fieldOfView = _fieldOfView_Settings.currentValue; }
            SubscribeToEvents();

            _fpsController.Initialize();
        }

        private void Update()
        {
            _fpsData.isChangeWeapon = Input.GetKeyUp(KeyCode.F);
            _fpsData.isReload = Input.GetKeyUp(KeyCode.R);
            _fpsData.isThwowGranade = Input.GetKeyUp(KeyCode.G);
            _fpsData.isRightLean = Input.GetKeyUp(KeyCode.E);
            _fpsData.leftLean = Input.GetKeyUp(KeyCode.Q);
            _fpsData.mouseScrollWeel = Input.GetAxis("Mouse ScrollWheel");
            _fpsData.isFirePressed = Input.GetKeyDown(KeyCode.Mouse0);
            _fpsData.isFireReleased = Input.GetKeyUp(KeyCode.Mouse0);
            _fpsData.isToggleAim = Input.GetKeyDown(KeyCode.Mouse1);
            _fpsData.isChangeScope = Input.GetKeyDown(KeyCode.V);
            _fpsData.isB = Input.GetKeyDown(KeyCode.B);
            _fpsData.isH = Input.GetKeyDown(KeyCode.H);
            _fpsData.isFreeLook = Input.GetKey(KeyCode.X);
            _fpsData.deltaMouseX = Input.GetAxis("Mouse X");
            _fpsData.deltaMouseY = -Input.GetAxis("Mouse Y");
            _fpsData.isJump = Input.GetKeyDown(KeyCode.Space);
            _fpsData.isSprint = Input.GetKey(KeyCode.LeftShift);
            _fpsData.isSlide = Input.GetKey(KeyCode.X);
            _fpsData.isProne = Input.GetKeyDown(KeyCode.LeftControl);
            _fpsData.isCrouch = Input.GetKeyDown(KeyCode.C);
            _fpsData.moveXRaw = Input.GetAxisRaw("Horizontal");
            _fpsData.moveYRaw = Input.GetAxisRaw("Vertical");
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

        public void OnHasDamaged(PostDamageInfo aDamage)
        {

        }
    }
}