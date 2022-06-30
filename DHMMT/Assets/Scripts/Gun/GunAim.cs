using Characters.States.Data;
using DG.Tweening;
using Samhereis.Values;
using UnityEngine;

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
        [SerializeField] private BoolValue_SO _aimed;

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
            _aimed.AddListener(Aim);
        }

        private void OnUnequip(HumanoidData sentEquipData)
        {
            _equipData = null;
            _aimed.RemoveListener(Aim);
        }

        public void Aim(bool isAimed)
        {
            if (isAimed == false) _equipData.weaponHolder.DOLocalMove(_initialPosition, _animationDuration);
            else _equipData.weaponHolder.DOLocalMove(_aimPosition, _animationDuration);
        }
    }
}