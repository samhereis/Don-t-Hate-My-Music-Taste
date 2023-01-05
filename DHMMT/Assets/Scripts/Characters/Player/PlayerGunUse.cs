using Agents;
using Gameplay;
using Helpers;
using Identifiers;
using Network;
using Photon.Pun;
using PlayerInputHolder;
using UnityEngine;
using UnityEngine.InputSystem;
using Values;

namespace Characters.States.Data
{
    public class PlayerGunUse : HumanoidAttackingStateData //TODO: complete this
    {
        [Header("Components")]
        [SerializeField] private AnimationAgent _animator;
        [SerializeField] private IdentifierBase _identifier;

        [Header("Guns")]
        [SerializeField] private InteractableEquipWeapon _defaultWeapon;
        [SerializeField] private InteractableEquipWeapon _firstWeapon;
        [SerializeField] private InteractableEquipWeapon _secodWeapon;

        [Header("SO")]
        [SerializeField] private BoolValue_SO _isShooting;

        [SerializeField] private Input_SO _inputContainer;
        private InputActions _input => _inputContainer.input;

        private int _velocityHashY, _velocityHashX;

        private void OnValidate()
        {
            if (_identifier == null)
            {
                _identifier = GetComponent<IdentifierBase>();
                this.TrySetDirty();
            }
        }

        private void Awake()
        {
            if (_defaultWeapon != null && _identifier.TryGet<PhotonView>().IsMine)
            {
                _firstWeapon = PhotonNetwork.Instantiate(_defaultWeapon.name, transform.position, Quaternion.identity).GetComponent<InteractableEquipWeapon>();
                _firstWeapon?.gameObject.GetPhotonView().RPC(nameof(_firstWeapon.RPC_Interact), RpcTarget.All, _identifier.TryGet<PhotonView>().ViewID);
            }

            _velocityHashY = Animator.StringToHash("moveVelocityY");
            _velocityHashX = Animator.StringToHash("moveVelocityX");
        }

        private void OnEnable()
        {
            if (_identifier.TryGet<PhotonView>().IsMine)
            {
                _input.Gameplay.Fire.performed += Fire;
                _input.Gameplay.Fire.canceled += Fire;

                _input.Gameplay.Reload.performed += Reload;

                _input.Gameplay.ChangeWeapon.performed += ChangeWeapon;

                _input.Gameplay.Sprint.performed += Sprint;
            }
        }

        private void OnDisable()
        {
            _input.Gameplay.Fire.performed -= Fire;
            _input.Gameplay.Fire.canceled -= Fire;

            _input.Gameplay.Reload.performed -= Reload;

            _input.Gameplay.ChangeWeapon.performed -= ChangeWeapon;

            _input.Gameplay.Sprint.performed -= Sprint;
        }

        public void ChangeWeapon(InputAction.CallbackContext context)
        {

        }

        private void Sprint(InputAction.CallbackContext context)
        {

        }

        private void Fire(InputAction.CallbackContext context)
        {
            _isShooting.ChangeValue(context.ReadValueAsButton());

            onAttack?.Invoke(_isShooting.value);

            _animator.gameObject.GetPhotonView().RPC(nameof(_animator.RPC_SetFloat), RpcTarget.All, _velocityHashY, 1f);
            _animator.gameObject.GetPhotonView().RPC(nameof(_animator.RPC_SetFloat), RpcTarget.All, _velocityHashX, 1f);
        }

        private void Reload(InputAction.CallbackContext context)
        {

        }
    }
}