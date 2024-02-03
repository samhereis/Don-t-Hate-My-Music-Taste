// Designed by Kinemation, 2023

using Kinemation.FPSFramework.Runtime.Camera;
using Kinemation.FPSFramework.Runtime.Core.Types;
using Kinemation.FPSFramework.Runtime.FPSAnimator;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Scripts.Runtime
{
    public enum OverlayType
    {
        Default,
        Pistol,
        Rifle
    }

    public class Weapon : FPSAnimWeapon
    {
        public Action onFire;

        [Header("Animations")]
        public AnimSequence reloadClip;
        public AnimSequence grenadeClip;
        public AnimSequence fireClip;
        public OverlayType overlayType;

        [Header("Aiming")]
        public bool canAim = true;
        [SerializeField] private List<Transform> scopes;

        [Header("Recoil")]
        public RecoilPattern recoilPattern;
        public FPSCameraShake cameraShake;

        private Animator _animator;
        private int _scopeIndex;

        protected void Start()
        {
            _animator = GetComponentInChildren<Animator>();
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

            onFire?.Invoke();
        }

        public void Reload()
        {
            if (_animator != null)
            {
                _animator.Rebind();
                _animator.Play("Reload", 0);
            }
        }
    }
}