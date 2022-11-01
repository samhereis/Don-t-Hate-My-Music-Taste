using Characters.States.Data;
using DG.Tweening;
using UnityEngine;
using Values;

namespace Gameplay
{
    public class GunAim : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector3 _aimPosition;
        [SerializeField] private Vector3 _initialPosition;
        [SerializeField] private float _animationDuration = 0.05f;

        [Header("Components")]
        [SerializeField] private InteractableEquipWeapon _interactableEquipWeapon;
        [SerializeField] private HumanoidData _equipData;
        [SerializeField] private BoolValue_SO _isAiming;

        private void Awake()
        {
            _interactableEquipWeapon = GetComponent<InteractableEquipWeapon>();
            _interactableEquipWeapon.onEquip.AddListener(OnEquip);
            _interactableEquipWeapon.onUnequip.AddListener(OnUnequip);
        }

        private void OnEquip(HumanoidData sentEquipData)
        {
            _equipData = sentEquipData;
            _equipData.weaponHolder.localPosition = _initialPosition;
            _isAiming.AddListener(Aim);
        }

        private void OnUnequip(HumanoidData sentEquipData)
        {
            _equipData = null;
            _isAiming.RemoveListener(Aim);
        }

        public void Aim(bool isAiming)
        {
            if (isAiming == false) _equipData.weaponHolder.DOLocalMove(_initialPosition, _animationDuration);
            else _equipData.weaponHolder.DOLocalMove(_aimPosition, _animationDuration);
        }
    }
}