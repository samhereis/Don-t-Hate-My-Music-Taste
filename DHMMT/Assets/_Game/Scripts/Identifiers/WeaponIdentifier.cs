using Agents;
using DataClasses;
using Demo.Scripts.Runtime;
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

        [Header("Components")]
        [SerializeField] private ISoundPlayer _soundPlayer;
        [SerializeField] private Weapon _weapon_;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private AnimationAgent _weaponAnimationAgent;
        [SerializeField] private AnimationAgent _actorAnimationAgent;

        [Header("SO")]
        [SerializeField] private BulletPooling_SO _bulletPooling_SO;

        private IDamagerActor _damagerActor;

        private Dictionary<string, Action> _animationAgentCallbackMethods = new Dictionary<string, Action>();

        public Weapon weapon
        {
            get
            {
                if (_weapon_ == null) { _weapon_ = GetComponentInChildren<Weapon>(true); };
                return _weapon_;
            }
        }

        private void Awake()
        {
            _soundPlayer = GetComponentInChildren<SoundPlayer>(true);
            _weaponAnimationAgent = GetComponentInChildren<AnimationAgent>(true);

            if (_shootPoint == null) { _shootPoint = transform.Find("ShootPoint"); }

            _animationAgentCallbackMethods.Add("OnReload", OnReload);
        }

        public void OnEquip(IDamagerActor damagerActor, AnimationAgent actorAnimationAgent)
        {
            _actorAnimationAgent = actorAnimationAgent;

            if (_actorAnimationAgent != null)
            {
                _actorAnimationAgent.onAnimationCallback -= OnAnimationEventRecieved;
                _actorAnimationAgent.onAnimationCallback += OnAnimationEventRecieved;
            }

            _damagerActor = damagerActor;

            weapon.onFire -= OnFire;
            onReloaded -= OnReloaded;

            weapon.onFire += OnFire;
            onReloaded += OnReloaded;
        }

        public void OnUnequip()
        {
            if (_actorAnimationAgent != null)
            {
                _actorAnimationAgent.onAnimationCallback -= OnAnimationEventRecieved;
                _actorAnimationAgent = null;
            }

            _damagerActor = null;

            weapon.onFire -= OnFire;
            onReloaded -= OnReloaded;
        }

        private void OnReloaded()
        {
            currentAmmo = maxAmmo;
            canShoot = true;
        }

        private void OnFire()
        {
            if (canShoot == false) { return; }

            var bullet = _bulletPooling_SO.PutOff(_shootPoint.position, _shootPoint.rotation);
            bullet.Initialize(_damagerActor);

            _soundPlayer?.TryPlay(_fireSound);

            currentAmmo--;

            if (currentAmmo <= 0) { canShoot = false; }
        }

        private void OnAnimationEventRecieved(string eventName)
        {
            Debug.Log(eventName);
            if (_animationAgentCallbackMethods.ContainsKey(eventName))
            {
                Action action = _animationAgentCallbackMethods[eventName];
                action?.Invoke();
            }
        }

        private void OnReload()
        {
            onReloaded?.Invoke();
        }
    }
}