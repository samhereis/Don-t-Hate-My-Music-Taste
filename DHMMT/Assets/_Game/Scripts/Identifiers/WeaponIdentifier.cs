using Agents;
using DataClasses;
using Demo.Scripts.Runtime;
using Helpers;
using Interfaces;
using Pooling;
using SO;
using Sounds;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Identifiers
{
    public class WeaponIdentifier : IdentifierBase
    {
        public Action onReloaded;

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
        [SerializeField] private ISoundPlayer _soundPlayer;
        [field: SerializeField] public Weapon _weapon { get; private set; }
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AnimationAgent _animationAgent;

        [Header("SO")]
        [SerializeField] private BulletPooling_SO _bulletPooling_SO;

        private IDamagerActor _damagerActor;

        private Dictionary<string, Action> _animationAgentCallbackMethods;

        private void Awake()
        {
            _soundPlayer = GetComponentInChildren<SoundPlayer>(true);
            _weapon = GetComponentInChildren<Weapon>(true);
            _animationAgent = GetComponentInChildren<AnimationAgent>(true);

            if(_shootPoint == null) { _shootPoint = transform.Find("ShootPoint"); }

            _animationAgentCallbackMethods = new Dictionary<string, Action>() { { "OnReload", onReloaded } };
        }

        public void OnEquip(IDamagerActor damagerActor)
        {
            _damagerActor = damagerActor;

            _weapon.onFire -= OnFire;
            onReloaded -= OnReloaded;

            _weapon.onFire += OnFire;
            onReloaded += OnReloaded;
        }

        public void OnUnequip()
        {
            _damagerActor = null;

            _weapon.onFire -= OnFire;
            onReloaded -= OnReloaded;
        }

        private void OnReloaded()
        {
            currentAmmo = maxAmmo;
            ResetCanShoot(1f);
        }

        private void OnFire()
        {
            if (canShoot == false) { return; }

            var bullet = _bulletPooling_SO.PutOff(_shootPoint.position, _shootPoint.rotation);
            bullet.Initialize(_damagerActor);

            _soundPlayer?.TryPlay(_fireSound);

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