using Characters.States.Data;
using Helpers;
using Identifiers;
using Network;
using Photon.Pun;
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

        private HumanoidData currentHumanoidData;

        private void OnEnable()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                _interactableEquipWeapon.onEquip.AddListener(RegisterEvents);
                _interactableEquipWeapon.onUnequip.AddListener(DegisterEvents);
            }
        }

        private void OnDisable()
        {
            _interactableEquipWeapon.onEquip.RemoveListener(RegisterEvents);
            _interactableEquipWeapon.onUnequip.RemoveListener(DegisterEvents);

            DegisterEvents(currentHumanoidData);
        }

        private void RegisterEvents(HumanoidData sentData)
        {
            DegisterEvents(currentHumanoidData);

            currentHumanoidData = sentData;

            _humanoidMovementStateData = currentHumanoidData.humanoidMovementStateData;
            _humanoidMovementStateData.onIsMovingChange += SetSpeed;
        }

        private void DegisterEvents(HumanoidData sentData)
        {
            if (_humanoidMovementStateData == null || currentHumanoidData == null) return;

            _humanoidMovementStateData.onIsMovingChange -= SetSpeed;
            _humanoidMovementStateData = null;
        }

        private void SetSpeed(bool value, float speed)
        {
            try
            {
                if (_isMoving == true && _isSpring == false)
                {
                    _animator?.SetFloat(_speed, speed);
                }
                else if (_isMoving == true && _isSpring == true)
                {
                    _animator?.SetFloat(_speed, speed * 2);
                }
                else
                {
                    _animator?.SetFloat(_speed, 0);
                }
            }
            finally
            {

            }
        }
    }
}