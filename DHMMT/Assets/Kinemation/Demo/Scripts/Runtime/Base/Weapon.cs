// Designed by Kinemation, 2023

using Agents;
using Identifiers;
using Interfaces;
using Kinemation.FPSFramework.Runtime.FPSAnimator;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Scripts.Runtime.Base
{
    public enum OverlayType
    {
        Default,
        Pistol,
        Rifle
    }

    public class Weapon : FPSAnimWeapon
    {
        public Action<WeaponIdentifier> onFire;
        public Action<WeaponIdentifier> onReloaded;

        public Action<WeaponIdentifier, IDamagerActor> onEquip;
        public Action<WeaponIdentifier> onUnequip;

        public AnimSequence reloadClip;
        public AnimSequence grenadeClip;
        public OverlayType overlayType;

        [SerializeField] private List<Transform> scopes;
        [SerializeField] private Animator _animator;
        private int _scopeIndex;

        [Header("Components")]
        [SerializeField] private AnimationAgent _animationAgent;
        [SerializeField] private WeaponIdentifier _weaponIdentifier;

        private Dictionary<string, Action> _animationAgentCallbackMethods;

        protected void Start()
        {
            if (_animator == null) _animator = GetComponentInChildren<Animator>();
            if (_animationAgent == null) _animationAgent = GetComponentInChildren<AnimationAgent>();
            if (_weaponIdentifier == null) _weaponIdentifier = GetComponentInChildren<WeaponIdentifier>();

            _animationAgentCallbackMethods = new Dictionary<string, Action>() { { "OnReload", OnReloaded } };
        }

        public void Initialize(IDamagerActor type)
        {

        }

        public override Transform GetAimPoint()
        {
            _scopeIndex++;
            _scopeIndex = _scopeIndex > scopes.Count - 1 ? 0 : _scopeIndex;
            return scopes[_scopeIndex];
        }

        public void OnFire()
        {
            if (_animator != null)
            {
                _animator.Play("Fire", 0, 0f);
            }

            onFire?.Invoke(_weaponIdentifier);
        }

        public void Reload()
        {
            if (_animator != null)
            {
                _animator.Play("Reload", 0, 0f);
            }
            else
            {
                OnReloaded();
            }
        }

        public void OnReloaded()
        {
            onReloaded?.Invoke(_weaponIdentifier);
        }

        public virtual void OnEquip(IDamagerActor damagerActor)
        {
            if (_animationAgent == null) _animationAgent = GetComponentInChildren<AnimationAgent>();
            if (_animationAgent != null)
            {
                _animationAgent.onAnimationCallback -= OnAnimationCallback;
                _animationAgent.onAnimationCallback += OnAnimationCallback;
            }

            onEquip?.Invoke(_weaponIdentifier, damagerActor);
        }

        public virtual void OnUnequip()
        {
            if (_animationAgent == null) _animationAgent = GetComponentInChildren<AnimationAgent>();
            if (_animationAgent != null) _animationAgent.onAnimationCallback -= OnAnimationCallback;

            onUnequip?.Invoke(_weaponIdentifier);

            onFire = null;
            onReloaded = null;
            onEquip = null;
            onUnequip = null;
        }

        private void OnAnimationCallback(string callback)
        {
            if (_animationAgentCallbackMethods.ContainsKey(callback))
            {
                _animationAgentCallbackMethods[callback]?.Invoke();
            }
        }
    }
}