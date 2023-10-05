using Demo.Scripts.Runtime.Base;
using Helpers;
using Identifiers;
using Interfaces;
using Kinemation.FPSFramework.Runtime.FPSAnimator;
using Kinemation.FPSFramework.Runtime.Layers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Charatcers.Enemy
{
    public class EnemyController : FPSAnimController
    {
        [SerializeField] private List<WeaponIdentifier> weapons;

        [Header("Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [SerializeField][HideInInspector] private LookLayer lookLayer;
        [SerializeField][HideInInspector] private AdsLayer adsLayer;
        [SerializeField][HideInInspector] private SwayLayer swayLayer;
        [SerializeField][HideInInspector] private LocomotionLayer locoLayer;

        private IDamagerActor _damagerActor;

        private FPSPoseState _poseState;
        private FPSMovementState _movementState;

        private int _weaponIndex;
        private float _speed;
        private bool _isReloading = false;

        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");

        private void Awake()
        {
            InitAnimController(UpdateCameraRotation);

            weapons = GetComponentsInChildren<WeaponIdentifier>(true).ToList();
            _damagerActor = GetComponentInChildren<IDamagerActor>(true);

            lookLayer = GetComponentInChildren<LookLayer>();
            adsLayer = GetComponentInChildren<AdsLayer>();
            locoLayer = GetComponentInChildren<LocomotionLayer>();
            swayLayer = GetComponentInChildren<SwayLayer>();

            foreach (var rigidBody in _animator.GetComponentsInChildren<Rigidbody>(true))
            {
                rigidBody.isKinematic = true;
            }
        }

        private void Start()
        {
            _weaponIndex = weapons.GetRandomIndex();
            EquipWeapon();
        }

        private void Update()
        {
            UpdateMovement();
            UpdateAnimController();
            UpdateLookInput();
        }

        private void UpdateCameraRotation()
        {

        }

        private void UpdateLookInput()
        {
            charAnimData.SetAimInput(Vector2.zero);
            charAnimData.AddDeltaInput(Vector2.zero);
        }

        private void EquipWeapon()
        {
            if (weapons.Count == 0) return;

            foreach (var weapon in weapons)
            {
                weapon.gameObject.SetActive(false);
                weapon.TryGet<Weapon>().OnUnequip();
            }

            GetGun().gameObject.SetActive(true);

            InitWeapon(GetGun());
            GetGun().OnEquip(_damagerActor);
        }

        public void Shoot(Vector3 targetPosition)
        {
            if (weapons.Count == 0) return;
            transform.LookAt(targetPosition);

            Fire();
        }

        private void Fire()
        {
            var gunIdentifier = GetGunIdentifier();

            if (gunIdentifier.canShoot == false)
            {
                recoilComponent.Stop();

                if (gunIdentifier.currentAmmo < 1)
                {
                    if (_isReloading == false)
                    {
                        Reload();
                    }
                    return;
                }
            }
            else
            {
                GetGun().OnFire();
                recoilComponent.Play();
            }
        }

        private void Reload()
        {
            if (GetGun().reloadClip != null)
            {
                _isReloading = true;
                PlayAnimation(GetGun().reloadClip);
            }

            GetGun().Reload();

            GetGun().onReloaded -= OnReloaded;
            GetGun().onReloaded += OnReloaded;
        }

        private void OnReloaded(WeaponIdentifier weapon)
        {
            GetGun().onReloaded -= OnReloaded;
            EquipWeapon();
            _isReloading = false;
        }

        private Weapon GetGun()
        {
            return weapons[_weaponIndex].TryGet<Weapon>();
        }

        private WeaponIdentifier GetGunIdentifier()
        {
            return weapons[_weaponIndex];
        }

        public void Sprint(float speed)
        {
            if (_poseState == FPSPoseState.Crouching)
            {
                return;
            }

            lookLayer.SetLayerAlpha(0.5f);
            adsLayer.SetLayerAlpha(0f);
            locoLayer.SetReadyWeight(0f);

            _movementState = FPSMovementState.Sprinting;

            recoilComponent.Stop();

            _speed = speed;
        }

        public void Stop(float speed)
        {
            if (_poseState == FPSPoseState.Crouching)
            {
                return;
            }

            lookLayer.SetLayerAlpha(1f);
            adsLayer.SetLayerAlpha(1f);

            _movementState = FPSMovementState.Walking;

            _speed = speed;
        }

        private void UpdateMovement()
        {
            _animator.SetBool(Moving, _speed != 0);
            _animator.SetFloat(MoveX, 0);
            _animator.SetFloat(MoveY, _speed);

            if (_movementState == FPSMovementState.Sprinting)
            {

            }
        }
    }
}