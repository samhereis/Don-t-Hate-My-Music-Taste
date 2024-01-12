using Demo.Scripts.Runtime.Base;
using Helpers;
using Interfaces;
using Pooling;
using SO;
using Sounds;
using UnityEngine;

namespace Identifiers
{
    public class WeaponIdentifier : IdentifierBase
    {
        public enum WeaponType { Rifle, Pistol, Grenade }

        [field: SerializeField] public WeaponType weaponType { get; private set; } = WeaponType.Rifle;

        [Header("Sounds")]
        [SerializeField] private Sound_String_SO _fireSound;
        [SerializeField] private Sound_String_SO _reloadSound;

        [field: SerializeField, Header("Ammo")] public int maxAmmo { get; private set; } = 30;
        [field: SerializeField] public int currentAmmo { get; private set; } = 30;
        [field: SerializeField] public bool canShoot { get; private set; } = true;
        [field: SerializeField] public float fireRate { get; private set; } = 0.1f;

        [Header("Components")]
        [SerializeField] private SoundPlayer _soundPlayer;
        [field: SerializeField] public Weapon _weapon { get; private set; }
        [SerializeField] private Transform _shootPoint;

        [Header("Prefabs")]
        [SerializeField] private CrosshairIdentifier _crosshairPrefab;

        [Header("SO")]
        [SerializeField] private BulletPooling_SO _bulletPooling_SO;

        private IDamagerActor _damagerActor;

        private void Awake()
        {
            _soundPlayer = GetComponentInChildren<SoundPlayer>(true);
            _weapon = GetComponentInChildren<Weapon>(true);
        }

        private void OnEnable()
        {
            _soundPlayer?.Setup();

            _weapon.onEquip += OnEquip;
            _weapon.onUnequip += OnUnequip;
        }

        private void OnDisable()
        {
            _weapon.onEquip -= OnEquip;
            _weapon.onUnequip -= OnUnequip;
        }

        private void OnEquip(WeaponIdentifier weapon, IDamagerActor damagerActor)
        {
            _damagerActor = damagerActor;

            _weapon.onFire -= OnFire;
            _weapon.onReloaded -= OnReload;

            _weapon.onFire += OnFire;
            _weapon.onReloaded += OnReload;
        }

        private void OnUnequip(WeaponIdentifier weapon)
        {
            _damagerActor = null;

            _weapon.onFire -= OnFire;
            _weapon.onReloaded -= OnReload;
        }

        private void OnReload(WeaponIdentifier weapon)
        {
            currentAmmo = maxAmmo;
            ResetCanShoot(1f);
        }

        private void OnFire(WeaponIdentifier weapon)
        {
            if (canShoot == false) { return; }

            var bullet = _bulletPooling_SO.PutOff(_shootPoint.position, _shootPoint.rotation);
            bullet.Initialize(_damagerActor);

            _soundPlayer.TryPlay(_fireSound);

            currentAmmo--;

            canShoot = false;
            ResetCanShoot(fireRate);
        }

        private async void ResetCanShoot(float delay)
        {
            await AsyncHelper.DelayFloat(delay);

            if (currentAmmo > 0) canShoot = true;
        }
    }
}