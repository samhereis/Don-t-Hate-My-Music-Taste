// Designed by KINEMATION, 2023

using Agents;
using DataClasses;
using Identifiers;
using Interfaces;
using Kinemation.FPSFramework.Runtime.FPSAnimator;
using Kinemation.FPSFramework.Runtime.Layers;
using Kinemation.FPSFramework.Runtime.Recoil;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Demo.Scripts.Runtime
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class TabAttribute : PropertyAttribute
    {
        public readonly string tabName;

        public TabAttribute(string tabName)
        {
            this.tabName = tabName;
        }
    }

    public enum FPSAimState
    {
        None,
        Ready,
        Aiming,
        PointAiming
    }

    public enum FPSActionState
    {
        None,
        Reloading,
        WeaponChange
    }

    // An example-controller class
    public class FPSController : FPSAnimController
    {
        public Action<bool> isPlayerAimingChangedEvent;
        public Action<WeaponIdentifier> onChangeWeapon;
        public Action<WeaponIdentifier> onPlayerShoot;
        public Action<WeaponIdentifier> onPlayerReloaded;

        [ShowInInspector] private IDamagerActor _damagerActor;
        [SerializeField] protected FPSData _fpsData;
        [SerializeField] private AnimationAgent _animationAgent;
        private DateTime lastShotTime;

        [Tab("Animation")]
        [SerializeField, FoldoutGroup("General")] private Animator animator;

        [SerializeField, FoldoutGroup("Turn In Place")] private float turnInPlaceAngle;
        [SerializeField, FoldoutGroup("Turn In Place")] private AnimationCurve turnCurve = new AnimationCurve(new Keyframe(0f, 0f));
        [SerializeField, FoldoutGroup("Turn In Place")] private float turnSpeed = 1f;

        [Header("Leaning")]
        [SerializeField, FoldoutGroup("Leaning")] private float smoothLeanStep = 1f;
        [SerializeField, FoldoutGroup("Leaning"), Range(0f, 1f)] private float startLean = 1f;

        [SerializeField, FoldoutGroup("Dynamic Motions")] private IKAnimation aimMotionAsset;
        [SerializeField, FoldoutGroup("Dynamic Motions")] private IKAnimation leanMotionAsset;
        [SerializeField, FoldoutGroup("Dynamic Motions")] private IKAnimation crouchMotionAsset;
        [SerializeField, FoldoutGroup("Dynamic Motions")] private IKAnimation unCrouchMotionAsset;
        [SerializeField, FoldoutGroup("Dynamic Motions")] private IKAnimation onJumpMotionAsset;
        [SerializeField, FoldoutGroup("Dynamic Motions")] private IKAnimation onLandedMotionAsset;
        [SerializeField, FoldoutGroup("Dynamic Motions")] private IKAnimation onStartStopMoving;
        [SerializeField, FoldoutGroup("Dynamic Motions")] private IKPose sprintPose;
        [SerializeField, FoldoutGroup("Dynamic Motions")] private IKPose pronePose;

        // Animation Layers
        [SerializeField][HideInInspector] private LookLayer lookLayer;
        [SerializeField][HideInInspector] private AdsLayer adsLayer;
        [SerializeField][HideInInspector] private SwayLayer swayLayer;
        [SerializeField][HideInInspector] private LocomotionLayer locoLayer;
        [SerializeField][HideInInspector] private SlotLayer slotLayer;
        [SerializeField][HideInInspector] private WeaponCollision collisionLayer;
        // Animation Layers

        [Tab("Controller")]
        [SerializeField, FoldoutGroup("Controller")] private float timeScale = 1f;
        [SerializeField, FoldoutGroup("Controller"), Min(0f)] private float equipDelay = 0f;

        [SerializeField, FoldoutGroup("Camera")] private Transform mainCamera;
        [SerializeField, FoldoutGroup("Camera")] private Transform cameraHolder;
        [SerializeField, FoldoutGroup("Camera")] private Transform firstPersonCamera;
        [SerializeField, FoldoutGroup("Camera")] private float sensitivity;
        [SerializeField, FoldoutGroup("Camera")] private Vector2 freeLookAngle;

        [SerializeField, FoldoutGroup("Movement")] private FPSMovement movementComponent;

        [SerializeField]
        [Tab("Weapon")]
        private List<WeaponIdentifier> weapons;
        private Vector2 _playerInput;

        // Used for free-look
        private Vector2 _freeLookInput;

        private int _currentWeaponIndex;
        private int _lastIndex;

        private int _bursts;
        private bool _freeLook;

        private FPSAimState aimState;
        private FPSActionState actionState;

        private static readonly int Crouching = Animator.StringToHash("Crouching");
        private static readonly int OverlayType = Animator.StringToHash("OverlayType");
        private static readonly int TurnRight = Animator.StringToHash("TurnRight");
        private static readonly int TurnLeft = Animator.StringToHash("TurnLeft");
        private static readonly int UnEquip = Animator.StringToHash("UnEquip");

        private Vector2 _controllerRecoil;
        private float _recoilStep;
        private bool _isFiring;

        private bool _isUnarmed;
        private float _lastRecoilTime;

        public void Initialize()
        {
            _damagerActor = GetComponent<IDamagerActor>();

            weapons = GetComponentsInChildren<WeaponIdentifier>(true).ToList();
            _animationAgent = GetComponentInChildren<AnimationAgent>(true);
            movementComponent = GetComponent<FPSMovement>();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            moveRotation = transform.rotation;

            movementComponent.onStartMoving.AddListener(OnMoveStarted);
            movementComponent.onStopMoving.AddListener(OnMoveEnded);

            movementComponent.onCrouch.AddListener(OnCrouch);
            movementComponent.onUncrouch.AddListener(OnUncrouch);

            movementComponent.onJump.AddListener(OnJump);
            movementComponent.onLanded.AddListener(OnLand);

            movementComponent.onSprintStarted.AddListener(OnSprintStarted);
            movementComponent.onSprintEnded.AddListener(OnSprintEnded);

            movementComponent.onSlideStarted.AddListener(OnSlideStarted);
            movementComponent.onSlideEnded.AddListener(OnSlideEnded);

            movementComponent.onProneStarted.AddListener(() => collisionLayer.SetLayerAlpha(0f));
            movementComponent.onProneEnded.AddListener(() => collisionLayer.SetLayerAlpha(1f));

            movementComponent.slideCondition += () => !HasActiveAction();
            movementComponent.sprintCondition += () => !HasActiveAction();
            movementComponent.proneCondition += () => !HasActiveAction();

            actionState = FPSActionState.None;

            InitLayers();
            EquipWeapon();
        }

        private void InitLayers()
        {
            InitAnimController();

            animator = GetComponentInChildren<Animator>();
            lookLayer = GetComponentInChildren<LookLayer>();
            adsLayer = GetComponentInChildren<AdsLayer>();
            locoLayer = GetComponentInChildren<LocomotionLayer>();
            swayLayer = GetComponentInChildren<SwayLayer>();
            slotLayer = GetComponentInChildren<SlotLayer>();
            collisionLayer = GetComponentInChildren<WeaponCollision>();
        }

        private bool HasActiveAction()
        {
            return actionState != FPSActionState.None;
        }

        private bool IsAiming()
        {
            return aimState is FPSAimState.Aiming or FPSAimState.PointAiming;
        }

        private void UnequipWeapon()
        {
            DisableAim();

            actionState = FPSActionState.WeaponChange;
            GetAnimGraph().GetFirstPersonAnimator().CrossFade(UnEquip, 0.1f);
        }

        public void ResetActionState()
        {
            actionState = FPSActionState.None;
        }

        public void RefreshStagedState()
        {
        }

        public void ResetStagedState()
        {
        }

        private void EquipWeapon()
        {
            if (weapons.Count == 0) return;

            foreach (var weapon in weapons)
            {
                weapon?.gameObject.SetActive(false);
            }

            var lastGun = weapons[_lastIndex];
            lastGun?.GetComponent<WeaponIdentifier>()?.OnUnequip();

            var gun = weapons[_currentWeaponIndex].TryGet<Weapon>();

            _bursts = gun.burstAmount;

            InitWeapon(gun);
            gun.gameObject.SetActive(true);

            animator.SetFloat(OverlayType, (float)gun.overlayType);
            actionState = FPSActionState.None;

            gun.GetComponent<WeaponIdentifier>()?.OnEquip(_damagerActor, _animationAgent);
        }

        private void EnableUnarmedState()
        {
            if (weapons.Count == 0) return;

            weapons[_currentWeaponIndex].gameObject.SetActive(false);
            animator.SetFloat(OverlayType, 0);
        }

        private void DisableAim()
        {
            if (!GetGun().TryGet<Weapon>().canAim) return;

            aimState = FPSAimState.None;
            OnInputAim(false);

            adsLayer.SetAds(false);
            adsLayer.SetPointAim(false);
            swayLayer.SetFreeAimEnable(true);
            swayLayer.SetLayerAlpha(1f);
        }

        public void ToggleAim()
        {
            if (!GetGun().TryGet<Weapon>().canAim) return;

            slotLayer.PlayMotion(aimMotionAsset);

            if (!IsAiming())
            {
                aimState = FPSAimState.Aiming;
                OnInputAim(true);

                adsLayer.SetAds(true);
                swayLayer.SetFreeAimEnable(false);
                swayLayer.SetLayerAlpha(0.5f);
            }
            else
            {
                DisableAim();
            }

            recoilComponent.isAiming = IsAiming();
        }

        public void ChangeScope()
        {
            InitAimPoint(GetGun().TryGet<Weapon>());
        }

        private void Fire()
        {
            if (HasActiveAction()) return;

            Weapon weapon = GetGun().TryGet<Weapon>();

            weapon.OnFire();
            PlayAnimation(weapon.fireClip);

            PlayCameraShake(weapon.cameraShake);

            if (GetGun().TryGet<Weapon>().recoilPattern != null)
            {
                float aimRatio = IsAiming() ? weapon.recoilPattern.aimRatio : 1f;
                float hRecoil = Random.Range(weapon.recoilPattern.horizontalVariation.x,
                    weapon.recoilPattern.horizontalVariation.y);
                _controllerRecoil += new Vector2(hRecoil, _recoilStep) * aimRatio;
            }

            if (recoilComponent == null || weapon.weaponAsset.recoilData == null)
            {
                return;
            }

            recoilComponent.Play();

            if (recoilComponent.fireMode == FireMode.Burst)
            {
                if (_bursts == 0)
                {
                    OnFireReleased();
                    return;
                }

                _bursts--;
            }

            if (recoilComponent.fireMode == FireMode.Semi)
            {
                _isFiring = false;
                return;
            }

            _recoilStep += weapon.recoilPattern.acceleration;
        }

        private void OnFirePressed()
        {
            if (weapons.Count == 0 || HasActiveAction()) return;

            if (Mathf.Approximately(GetGun().TryGet<Weapon>().fireRate, 0f))
            {
                return;
            }

            _lastRecoilTime = Time.unscaledTime;
            _bursts = GetGun().TryGet<Weapon>().burstAmount - 1;

            if (GetGun().TryGet<Weapon>().recoilPattern != null)
            {
                _recoilStep = GetGun().TryGet<Weapon>().recoilPattern.step;
            }

            _isFiring = true;
            lastShotTime = DateTime.Now;
        }

        private WeaponIdentifier GetGun()
        {
            if (weapons.Count == 0) return null;

            return weapons[_currentWeaponIndex];
        }

        private void OnFireReleased()
        {
            if (weapons.Count == 0) return;

            if (recoilComponent != null)
            {
                recoilComponent.Stop();
            }

            _recoilStep = 0f;
            _isFiring = false;
        }

        private void OnMoveStarted()
        {
            if (slotLayer != null)
            {
                slotLayer.PlayMotion(onStartStopMoving);
            }

            if (movementComponent.PoseState == FPSPoseState.Prone)
            {
                locoLayer.BlendInIkPose(pronePose);
            }
        }

        private void OnMoveEnded()
        {
            if (slotLayer != null)
            {
                slotLayer.PlayMotion(onStartStopMoving);
            }

            if (movementComponent.PoseState == FPSPoseState.Prone)
            {
                locoLayer.BlendOutIkPose();
            }
        }

        private void OnSlideStarted()
        {
            lookLayer.SetLayerAlpha(0.3f);
            GetAnimGraph().GetBaseAnimator().CrossFade("Sliding", 0.1f);
        }

        private void OnSlideEnded()
        {
            lookLayer.SetLayerAlpha(1f);
        }

        private void OnSprintStarted()
        {
            OnFireReleased();
            DisableAim();

            lookLayer.SetLayerAlpha(0.5f);

            if (GetGun().TryGet<Weapon>().overlayType == Runtime.OverlayType.Rifle)
            {
                locoLayer.BlendInIkPose(sprintPose);
            }

            aimState = FPSAimState.None;

            if (recoilComponent != null)
            {
                recoilComponent.Stop();
            }
        }

        private void OnSprintEnded()
        {
            lookLayer.SetLayerAlpha(1f);
            adsLayer.SetLayerAlpha(1f);
            locoLayer.BlendOutIkPose();
        }

        private void OnCrouch()
        {
            lookLayer.SetPelvisWeight(0f);
            animator.SetBool(Crouching, true);
            slotLayer.PlayMotion(crouchMotionAsset);

            GetAnimGraph().GetFirstPersonAnimator().SetFloat("MovementPlayRate", .7f);
        }

        private void OnUncrouch()
        {
            lookLayer.SetPelvisWeight(1f);
            animator.SetBool(Crouching, false);
            slotLayer.PlayMotion(unCrouchMotionAsset);

            GetAnimGraph().GetFirstPersonAnimator().SetFloat("MovementPlayRate", 1f);
        }

        private void OnJump()
        {
            slotLayer.PlayMotion(onJumpMotionAsset);
        }

        private void OnLand()
        {
            slotLayer.PlayMotion(onLandedMotionAsset);
        }

        private void TryReload()
        {
            if (HasActiveAction()) return;

            var reloadClip = GetGun().TryGet<Weapon>().reloadClip;

            if (reloadClip == null) return;

            OnFireReleased();

            PlayAnimation(reloadClip);
            GetGun().TryGet<Weapon>().Reload();
            actionState = FPSActionState.Reloading;
        }

        private void TryGrenadeThrow()
        {
            if (HasActiveAction()) return;
            if (GetGun().TryGet<Weapon>().grenadeClip == null) return;

            OnFireReleased();
            DisableAim();
            PlayAnimation(GetGun().TryGet<Weapon>().grenadeClip);
            actionState = FPSActionState.Reloading;
        }

        private bool _isLeaning;

        private void ChangeWeapon_Internal(int newIndex)
        {
            if (newIndex == _currentWeaponIndex || newIndex > weapons.Count - 1)
            {
                return;
            }

            _lastIndex = _currentWeaponIndex;
            _currentWeaponIndex = newIndex;

            OnFireReleased();

            UnequipWeapon();
            Invoke(nameof(EquipWeapon), equipDelay);
        }

        private void HandleWeaponChangeInput()
        {
            if (movementComponent.PoseState == FPSPoseState.Prone) return;
            if (HasActiveAction() || weapons.Count == 0) return;

            if (_fpsData.isChangeWeapon)
            {
                ChangeWeapon_Internal(_currentWeaponIndex + 1 > weapons.Count - 1 ? 0 : _currentWeaponIndex + 1);
                return;
            }
        }

        private void UpdateActionInput()
        {
            if (movementComponent.MovementState == FPSMovementState.Sprinting)
            {
                return;
            }

            if (_fpsData.isReload)
            {
                TryReload();
            }

            if (_fpsData.isThwowGranade)
            {
                TryGrenadeThrow();
            }

            HandleWeaponChangeInput();

            if (aimState != FPSAimState.Ready)
            {
                bool wasLeaning = _isLeaning;

                bool rightLean = _fpsData.isRightLean;
                bool leftLean = _fpsData.leftLean;

                _isLeaning = rightLean || leftLean;

                if (_isLeaning != wasLeaning)
                {
                    slotLayer.PlayMotion(leanMotionAsset);
                    charAnimData.SetLeanInput(wasLeaning ? 0f : rightLean ? -startLean : startLean);
                }

                if (_isLeaning)
                {
                    float leanValue = _fpsData.mouseScrollWeel * smoothLeanStep;
                    charAnimData.AddLeanInput(leanValue);
                }

                if (_fpsData.isFirePressed)
                {
                    if (GetGun().canShoot == true)
                    {
                        OnFirePressed();
                    }
                    else
                    {
                        TryReload();
                    }
                }

                if (_fpsData.isFireReleased)
                {
                    OnFireReleased();
                }

                if (_isFiring)
                {
                    if (GetGun().canShoot == true)
                    {
                        DateTime currentTime = DateTime.Now;
                        if ((currentTime - lastShotTime).TotalSeconds >= 1f / GetGun().weapon.fireRate)
                        {
                            Fire();
                            lastShotTime = currentTime;
                        }
                    }
                    else
                    {
                        TryReload();
                    }
                }

                if (_fpsData.isToggleAim)
                {
                    ToggleAim();
                }

                if (_fpsData.isChangeScope)
                {
                    ChangeScope();
                }

                if (_fpsData.isB && IsAiming())
                {
                    if (aimState == FPSAimState.PointAiming)
                    {
                        adsLayer.SetPointAim(false);
                        aimState = FPSAimState.Aiming;
                    }
                    else
                    {
                        adsLayer.SetPointAim(true);
                        aimState = FPSAimState.PointAiming;
                    }
                }
            }

            if (_fpsData.isH)
            {
                if (aimState == FPSAimState.Ready)
                {
                    aimState = FPSAimState.None;
                    lookLayer.SetLayerAlpha(1f);
                }
                else
                {
                    aimState = FPSAimState.Ready;
                    lookLayer.SetLayerAlpha(.5f);
                    OnFireReleased();
                }
            }
        }

        private Quaternion desiredRotation;
        private Quaternion moveRotation;
        private float turnProgress = 1f;
        private bool isTurning = false;

        private void TurnInPlace()
        {
            float turnInput = _playerInput.x;
            _playerInput.x = Mathf.Clamp(_playerInput.x, -90f, 90f);
            turnInput -= _playerInput.x;

            float sign = Mathf.Sign(_playerInput.x);
            if (Mathf.Abs(_playerInput.x) > turnInPlaceAngle)
            {
                if (!isTurning)
                {
                    turnProgress = 0f;

                    animator.ResetTrigger(TurnRight);
                    animator.ResetTrigger(TurnLeft);

                    animator.SetTrigger(sign > 0f ? TurnRight : TurnLeft);
                }

                isTurning = true;
            }

            transform.rotation *= Quaternion.Euler(0f, turnInput, 0f);

            float lastProgress = turnCurve.Evaluate(turnProgress);
            turnProgress += Time.deltaTime * turnSpeed;
            turnProgress = Mathf.Min(turnProgress, 1f);

            float deltaProgress = turnCurve.Evaluate(turnProgress) - lastProgress;

            _playerInput.x -= sign * turnInPlaceAngle * deltaProgress;

            transform.rotation *= Quaternion.Slerp(Quaternion.identity,
                Quaternion.Euler(0f, sign * turnInPlaceAngle, 0f), deltaProgress);

            if (Mathf.Approximately(turnProgress, 1f) && isTurning)
            {
                isTurning = false;
            }
        }

        private float _jumpState = 0f;

        private void UpdateLookInput()
        {
            if (movementComponent.isMainPlayer == false) { return; }

            _freeLook = _fpsData.isFreeLook;

            float deltaMouseX = _fpsData.deltaMouseX * sensitivity;
            float deltaMouseY = _fpsData.deltaMouseY * sensitivity;

            if (_freeLook)
            {
                // No input for both controller and animation component. We only want to rotate the camera

                _freeLookInput.x += deltaMouseX;
                _freeLookInput.y += deltaMouseY;

                _freeLookInput.x = Mathf.Clamp(_freeLookInput.x, -freeLookAngle.x, freeLookAngle.x);
                _freeLookInput.y = Mathf.Clamp(_freeLookInput.y, -freeLookAngle.y, freeLookAngle.y);

                return;
            }

            _freeLookInput = Vector2.Lerp(_freeLookInput, Vector2.zero,
                FPSAnimLib.ExpDecayAlpha(15f, Time.deltaTime));

            _playerInput.x += deltaMouseX;
            _playerInput.y += deltaMouseY;

            float proneWeight = animator.GetFloat("ProneWeight");
            Vector2 pitchClamp = Vector2.Lerp(new Vector2(-90f, 90f), new Vector2(-30, 0f), proneWeight);

            _playerInput.y = Mathf.Clamp(_playerInput.y, pitchClamp.x, pitchClamp.y);
            moveRotation *= Quaternion.Euler(0f, deltaMouseX, 0f);
            TurnInPlace();

            _jumpState = Mathf.Lerp(_jumpState, movementComponent.IsInAir() ? 1f : 0f,
                FPSAnimLib.ExpDecayAlpha(10f, Time.deltaTime));

            float moveWeight = Mathf.Clamp01(movementComponent.AnimatorVelocity.magnitude);
            transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, moveWeight);
            transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, _jumpState);
            _playerInput.x *= 1f - moveWeight;
            _playerInput.x *= 1f - _jumpState;

            charAnimData.SetAimInput(_playerInput);
            charAnimData.AddDeltaInput(new Vector2(deltaMouseX, charAnimData.deltaAimInput.y));
        }

        private Quaternion lastRotation;
        private Vector2 _cameraRecoilOffset;

        private void UpdateRecoil()
        {
            if (Mathf.Approximately(_controllerRecoil.magnitude, 0f)
                && Mathf.Approximately(_cameraRecoilOffset.magnitude, 0f))
            {
                return;
            }

            float smoothing = 8f;
            float restoreSpeed = 8f;
            float cameraWeight = 0f;

            if (GetGun().TryGet<Weapon>().recoilPattern != null)
            {
                smoothing = GetGun().TryGet<Weapon>().recoilPattern.smoothing;
                restoreSpeed = GetGun().TryGet<Weapon>().recoilPattern.cameraRestoreSpeed;
                cameraWeight = GetGun().TryGet<Weapon>().recoilPattern.cameraWeight;
            }

            _controllerRecoil = Vector2.Lerp(_controllerRecoil, Vector2.zero,
                FPSAnimLib.ExpDecayAlpha(smoothing, Time.deltaTime));

            _playerInput += _controllerRecoil * Time.deltaTime;

            Vector2 clamp = Vector2.Lerp(Vector2.zero, new Vector2(90f, 90f), cameraWeight);
            _cameraRecoilOffset -= _controllerRecoil * Time.deltaTime;
            _cameraRecoilOffset = Vector2.ClampMagnitude(_cameraRecoilOffset, clamp.magnitude);

            if (_isFiring) return;

            _cameraRecoilOffset = Vector2.Lerp(_cameraRecoilOffset, Vector2.zero,
                FPSAnimLib.ExpDecayAlpha(restoreSpeed, Time.deltaTime));
        }

        public void OnWarpStarted()
        {
            movementComponent.enabled = false;
            GetComponent<CharacterController>().enabled = false;
        }

        public void OnWarpEnded()
        {
            movementComponent.enabled = true;
            GetComponent<CharacterController>().enabled = true;
        }

        private void Update()
        {
            Time.timeScale = timeScale;

            UpdateActionInput();
            UpdateLookInput();
            UpdateRecoil();

            charAnimData.moveInput = movementComponent.AnimatorVelocity;
            UpdateAnimController();
        }

        public void UpdateCameraRotation()
        {
            Vector2 input = _playerInput;
            input += _cameraRecoilOffset;

            (Quaternion, Vector3) cameraTransform =
                (transform.rotation * Quaternion.Euler(input.y, input.x, 0f),
                    firstPersonCamera.position);

            cameraHolder.rotation = cameraTransform.Item1;
            cameraHolder.position = cameraTransform.Item2;

            mainCamera.rotation = cameraHolder.rotation * Quaternion.Euler(_freeLookInput.y, _freeLookInput.x, 0f);
        }
    }
}