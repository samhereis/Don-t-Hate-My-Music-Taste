using Characters.States.Data;
using UnityEngine;

namespace Gameplay
{
    public class WeaponAnimationController : MonoBehaviour
    {
        private const string _speed = "Speed";

        private bool _isSpring => _humanoidMovementStateData.isSprinting;
        private bool _isMoving => _humanoidMovementStateData.isMoving;

        [SerializeField] private Animator _animator;
        [SerializeField] private InteractableEquipWeapon _interactableEquipWeapon;
        [SerializeField] private HumanoidMovementStateData _humanoidMovementStateData;

        [Header("Debug")]
        [SerializeField] float _currentSpeed;

        private void Awake()
        {
            _interactableEquipWeapon.onEquip.AddListener(RegisterEvents);
            _interactableEquipWeapon.onUnequip.AddListener(DegisterEvents);
        }

        private void RegisterEvents(HumanoidData sentData)
        {
            _humanoidMovementStateData = sentData.humanoidMovementStateData;
            _humanoidMovementStateData.onIsMovingChange += SetSpeed;
        }

        private void DegisterEvents(HumanoidData sentData)
        {
            _humanoidMovementStateData.onIsMovingChange -= SetSpeed;
            _humanoidMovementStateData = null;
        }

        public void SetSpeed(bool value, float speed)
        {
            if (_isMoving == true && _isSpring == false)
            {
                _animator.SetFloat(_speed, speed);
            }
            else if (_isMoving == true && _isSpring == true)
            {
                _animator.SetFloat(_speed, speed * 2);
            }
            else
            {
                _animator.SetFloat(_speed, 0);
            }
        }
    }
}