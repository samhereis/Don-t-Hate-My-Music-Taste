// Designed by KINEMATION, 2023

using DataClasses;
using Kinemation.FPSFramework.Runtime.FPSAnimator;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Demo.Scripts.Runtime
{
    public enum FPSMovementState
    {
        Idle,
        Walking,
        Sprinting,
        InAir,
        Sliding
    }

    public enum FPSPoseState
    {
        Standing,
        Crouching,
        Prone
    }

    public class FPSMovement : MonoBehaviour
    {
        [SerializeField] private FPSData _fpsData;
        [field: SerializeField] public bool isMainPlayer { get; private set; } = false;

        public delegate bool ConditionDelegate();

        [FoldoutGroup("Events"), SerializeField] private FPSMovementSettings movementSettings;
        [FoldoutGroup("Events"), SerializeField] public Transform rootBone;

        [FoldoutGroup("Events"), SerializeField] public UnityEvent onStartMoving;
        [FoldoutGroup("Events"), SerializeField] public UnityEvent onStopMoving;

        [FoldoutGroup("Events"), SerializeField] public UnityEvent onSprintStarted;
        [FoldoutGroup("Events"), SerializeField] public UnityEvent onSprintEnded;

        [FoldoutGroup("Events"), SerializeField] public UnityEvent onCrouch;
        [FoldoutGroup("Events"), SerializeField] public UnityEvent onUncrouch;

        [FoldoutGroup("Events"), SerializeField] public UnityEvent onProneStarted;
        [FoldoutGroup("Events"), SerializeField] public UnityEvent onProneEnded;

        [FoldoutGroup("Events"), SerializeField] public UnityEvent onJump;
        [FoldoutGroup("Events"), SerializeField] public UnityEvent onLanded;

        [FoldoutGroup("Events"), SerializeField] public UnityEvent onSlideStarted;
        [FoldoutGroup("Events"), SerializeField] public UnityEvent onSlideEnded;

        public ConditionDelegate slideCondition;
        public ConditionDelegate proneCondition;
        public ConditionDelegate sprintCondition;

        public FPSMovementState MovementState { get; private set; }
        public FPSPoseState PoseState { get; private set; }

        public Vector2 AnimatorVelocity { get; private set; }

        private CharacterController _controller;
        private Animator _animator;
        private Vector2 _inputDirection;

        public Vector3 MoveVector { get; private set; }

        private Vector3 _velocity;

        private float _originalHeight;
        private Vector3 _originalCenter;

        private GaitSettings _desiredGait;
        private float _slideProgress = 0f;

        private Vector3 _prevPosition;
        private Vector3 _velocityVector;

        private bool _wantsToJump = false;

        private static readonly int InAir = Animator.StringToHash("InAir");
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Velocity = Animator.StringToHash("Velocity");
        private static readonly int Moving = Animator.StringToHash("Moving");
        private static readonly int Crouching = Animator.StringToHash("Crouching");
        private static readonly int Sliding = Animator.StringToHash("Sliding");
        private static readonly int Sprinting = Animator.StringToHash("Sprinting");
        private static readonly int Proning = Animator.StringToHash("Proning");

        private float _sprintAnimatorInterp = 8f;

        private bool _wasMoving = false;

        public bool IsInAir()
        {
            if (isMainPlayer == false) { return false; }
            return !_controller.isGrounded;
        }

        private bool IsMoving()
        {
            return !Mathf.Approximately(_inputDirection.normalized.magnitude, 0f);
        }

        private float GetSpeedRatio()
        {
            return _velocity.magnitude / _desiredGait.velocity;
        }

        private bool CanSlide()
        {
            return slideCondition == null || slideCondition.Invoke();
        }

        private bool CanSprint()
        {
            return sprintCondition == null || sprintCondition.Invoke();
        }

        private bool CanProne()
        {
            return proneCondition == null || proneCondition.Invoke();
        }

        private bool TryJump()
        {
            if (!_fpsData.isJump || PoseState == FPSPoseState.Crouching)
            {
                return false;
            }

            if (PoseState == FPSPoseState.Prone)
            {
                CancelProne();
                return false;
            }

            _wantsToJump = true;
            MovementState = FPSMovementState.InAir;
            return true;
        }

        private bool TrySprint()
        {
            if (PoseState is FPSPoseState.Crouching or FPSPoseState.Prone)
            {
                return false;
            }

            if (_inputDirection.y <= 0f || _inputDirection.x != 0f || !_fpsData.isSprint)
            {
                return false;
            }

            if (_fpsData.isSlide && GetSpeedRatio() > 0.5f)
            {
                if (!CanSlide()) return false;

                MovementState = FPSMovementState.Sliding;
                return true;
            }

            if (!CanSprint()) return false;

            MovementState = FPSMovementState.Sprinting;
            return true;
        }

        private bool CanUnCrouch()
        {
            float height = _originalHeight - _controller.radius * 2f;
            Vector3 position = rootBone.TransformPoint(_originalCenter + Vector3.up * height / 2f);
            return !Physics.CheckSphere(position, _controller.radius);
        }

        private void EnableProne()
        {
            Crouch();
            PoseState = FPSPoseState.Prone;
            _animator.SetBool(Crouching, false);
            _animator.SetBool(Proning, true);

            onProneStarted?.Invoke();
            _desiredGait = movementSettings.prone;
        }

        private void CancelProne()
        {
            if (!CanUnCrouch()) return;
            UnCrouch();
            PoseState = FPSPoseState.Standing;
            _animator.SetBool(Proning, false);

            onProneEnded?.Invoke();
            _desiredGait = movementSettings.walking;
        }

        private void Crouch()
        {
            float crouchedHeight = _originalHeight * movementSettings.crouchRatio;
            float heightDifference = _originalHeight - crouchedHeight;

            _controller.height = crouchedHeight;

            // Adjust the center position so the bottom of the capsule remains at the same position
            Vector3 crouchedCenter = _originalCenter;
            crouchedCenter.y -= heightDifference / 2;
            _controller.center = crouchedCenter;

            PoseState = FPSPoseState.Crouching;

            _animator.SetBool(Crouching, true);
            onCrouch.Invoke();
        }

        private void UnCrouch()
        {
            _controller.height = _originalHeight;
            _controller.center = _originalCenter;

            PoseState = FPSPoseState.Standing;

            _animator.SetBool(Crouching, false);
            onUncrouch.Invoke();
        }

        private void UpdatePoseState()
        {
            if (MovementState is FPSMovementState.Sprinting or FPSMovementState.InAir)
            {
                return;
            }

            if (_fpsData.isProne)
            {
                if (!CanProne())
                {
                    return;
                }

                if (PoseState == FPSPoseState.Prone)
                {
                    CancelProne();
                }
                else
                {
                    EnableProne();
                }

                return;
            }

            if (!_fpsData.isCrouch)
            {
                return;
            }

            if (PoseState == FPSPoseState.Standing)
            {
                Crouch();

                _desiredGait = movementSettings.crouching;
                return;
            }

            if (!CanUnCrouch()) return;

            UnCrouch();
            _desiredGait = movementSettings.walking;
        }

        private void UpdateMovementState()
        {
            if (MovementState == FPSMovementState.InAir && IsInAir())
            {
                // Do not update player movement while jumping or falling
                return;
            }

            if (MovementState != FPSMovementState.InAir && IsInAir())
            {
                MovementState = FPSMovementState.InAir;
                _wantsToJump = false;
                return;
            }

            // Get the current player input
            float moveX = _fpsData.moveXRaw;
            float moveY = _fpsData.moveYRaw;

            _inputDirection.x = moveX;
            _inputDirection.y = moveY;

            if (MovementState == FPSMovementState.Sliding && !Mathf.Approximately(_slideProgress, 1f))
            {
                // Consume input, but do not allow cancelling sliding.
                return;
            }

            // Jump action overrides any other input
            if (TryJump())
            {
                return;
            }

            if (TrySprint())
            {
                return;
            }

            if (!IsMoving())
            {
                MovementState = FPSMovementState.Idle;
                return;
            }

            MovementState = FPSMovementState.Walking;
        }

        private void OnMovementStateChanged(FPSMovementState prevState)
        {
            if (prevState == FPSMovementState.InAir)
            {
                onLanded.Invoke();
            }

            if (prevState == FPSMovementState.Sprinting)
            {
                _sprintAnimatorInterp = 7f;
                onSprintEnded.Invoke();
            }

            if (prevState == FPSMovementState.Sliding)
            {
                _sprintAnimatorInterp = 15f;
                onSlideEnded.Invoke();

                if (CanUnCrouch())
                {
                    UnCrouch();
                }
            }

            if (MovementState == FPSMovementState.Idle)
            {
                float prevVelocity = _desiredGait.velocity;
                _desiredGait = movementSettings.idle;
                _desiredGait.velocity = prevVelocity;
                return;
            }

            if (MovementState == FPSMovementState.InAir)
            {
                if (_wantsToJump)
                {
                    _velocity.y = movementSettings.jumpHeight;
                    onJump.Invoke();
                }

                return;
            }

            if (MovementState == FPSMovementState.Sprinting)
            {
                _desiredGait = movementSettings.sprinting;
                onSprintStarted.Invoke();
                return;
            }

            if (MovementState == FPSMovementState.Sliding)
            {
                _desiredGait.velocitySmoothing = movementSettings.slideDirectionSmoothing;
                _slideProgress = 0f;
                onSlideStarted.Invoke();
                Crouch();
                return;
            }

            if (PoseState == FPSPoseState.Crouching)
            {
                _desiredGait = movementSettings.crouching;
                return;
            }

            if (PoseState == FPSPoseState.Prone)
            {
                _desiredGait = movementSettings.prone;
                return;
            }

            // Walking state
            _desiredGait = movementSettings.walking;
        }

        private void UpdateSliding()
        {
            // 1. Extract the slide animation.
            float slideAmount = movementSettings.slideCurve.Evaluate(_slideProgress);

            // 2. Apply sliding to both current and desired velocity vectors.
            // Here we just want to interpolate between the same velocities, but different directions.

            _velocity *= slideAmount;

            Vector3 desiredVelocity = _velocity;
            desiredVelocity.y = -movementSettings.gravity;
            MoveVector = desiredVelocity;

            _slideProgress = Mathf.Clamp01(_slideProgress + Time.deltaTime * movementSettings.slideSpeed);
        }

        private void UpdateGrounded()
        {
            var normInput = _inputDirection.normalized;
            var desiredVelocity = rootBone.right * normInput.x + rootBone.forward * normInput.y;

            desiredVelocity *= _desiredGait.velocity;

            desiredVelocity = Vector3.Lerp(_velocity, desiredVelocity,
                FPSAnimLib.ExpDecayAlpha(_desiredGait.velocitySmoothing, Time.deltaTime));

            _velocity = desiredVelocity;

            desiredVelocity.y = -movementSettings.gravity;
            MoveVector = desiredVelocity;
        }

        private void UpdateInAir()
        {
            var normInput = _inputDirection.normalized;
            _velocity.y -= movementSettings.gravity * Time.deltaTime;
            _velocity.y = Mathf.Max(-movementSettings.maxFallVelocity, _velocity.y);

            var desiredVelocity = rootBone.right * normInput.x + rootBone.forward * normInput.y;
            desiredVelocity *= _desiredGait.velocity;

            desiredVelocity = Vector3.Lerp(_velocity, desiredVelocity * movementSettings.airFriction,
                FPSAnimLib.ExpDecayAlpha(movementSettings.airVelocity, Time.deltaTime));

            desiredVelocity.y = _velocity.y;
            _velocity = desiredVelocity;

            MoveVector = desiredVelocity;
        }

        private void UpdateMovement()
        {
            if (isMainPlayer == true) { _controller.Move(MoveVector * Time.deltaTime); }
        }

        private void UpdateAnimatorParams()
        {
            var animatorVelocity = _inputDirection;
            animatorVelocity *= MovementState == FPSMovementState.InAir ? 0f : 1f;

            AnimatorVelocity = Vector2.Lerp(AnimatorVelocity, animatorVelocity,
                FPSAnimLib.ExpDecayAlpha(_desiredGait.velocitySmoothing, Time.deltaTime));

            _animator.SetFloat(MoveX, AnimatorVelocity.x);
            _animator.SetFloat(MoveY, AnimatorVelocity.y);
            _animator.SetFloat(Velocity, AnimatorVelocity.magnitude);
            _animator.SetBool(InAir, IsInAir());
            _animator.SetBool(Moving, IsMoving());

            // Sprinting needs to be blended manually
            float a = _animator.GetFloat(Sprinting);
            float b = MovementState == FPSMovementState.Sprinting ? 1f : 0f;

            a = Mathf.Lerp(a, b, FPSAnimLib.ExpDecayAlpha(_sprintAnimatorInterp, Time.deltaTime));
            _animator.SetFloat(Sprinting, a);
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();

            _originalHeight = _controller.height;
            _originalCenter = _controller.center;

            MovementState = FPSMovementState.Idle;
            PoseState = FPSPoseState.Standing;

            _desiredGait = movementSettings.walking;
        }

        private void Update()
        {
            var prevState = MovementState;
            UpdateMovementState();
            UpdatePoseState();

            if (prevState != MovementState)
            {
                OnMovementStateChanged(prevState);
            }

            bool isMoving = IsMoving();

            if (_wasMoving != isMoving)
            {
                if (isMoving)
                {
                    onStartMoving?.Invoke();
                }
                else
                {
                    onStopMoving?.Invoke();
                }
            }

            _wasMoving = isMoving;

            if (MovementState == FPSMovementState.InAir)
            {
                UpdateInAir();
            }
            else if (MovementState == FPSMovementState.Sliding)
            {
                UpdateSliding();
            }
            else
            {
                UpdateGrounded();
            }

            UpdateMovement();
            UpdateAnimatorParams();
        }
    }
}