using System.Linq;
using Characters.States.Data;
using Identifiers;
using Interfaces;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

namespace Gameplay
{
    public class InteractableEquipWeapon : MonoBehaviourPunCallbacks, IInteractable
    {
        public bool isInteractable => !_equiped;
        public string ItemName { get { return _itemName; } private set { _itemName = value; } }

        [SerializeField] private bool _equiped = false;
        [SerializeField] private string _itemName = "Head Swaper";

        [Header("Inverse Kinematics Tramsform")]
        [SerializeField] private Transform _rightHandIK;
        [SerializeField] private Transform _leftHandIK;

        [Header("Components")]
        [SerializeField] private Animator _animatorComponent;
        [SerializeField] private Rigidbody _rigidbodyComponent;
        [SerializeField] private Collider _colliderComponent;

        [Header("Events")]
        public readonly UnityEvent<HumanoidData> onEquip = new UnityEvent<HumanoidData>();
        public readonly UnityEvent<HumanoidData> onUnequip = new UnityEvent<HumanoidData>();

        [PunRPC]

        public void RPC_Interact(int identifierViewId)
        {
            var allPhotonViews = FindObjectsOfType<PhotonView>(true).ToList();

            if (allPhotonViews.Count > 1)
            {
                var photonView = allPhotonViews.Find(x => x.ViewID == identifierViewId);

                if (photonView != null && photonView.TryGetComponent<IdentifierBase>(out var identifier))
                {
                    Interact(identifier);
                }
            }
        }
        public void Interact(IdentifierBase caller) // in other words - equiod this weapon to the humanoid
        {
            HumanoidData equipData = caller.TryGet<HumanoidData>();
            Animator animator = caller.TryGet<Animator>();

            _equiped = true;

            {   //Manage Position and Rotation
                transform.SetParent(equipData.weaponHolder, false);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }

            {   //Manage RigidBody
                _rigidbodyComponent.isKinematic = true;
                _rigidbodyComponent.useGravity = false;

                _colliderComponent.enabled = false;
            }

            {   //Manage player animation layer
                animator.SetBool("hands", false);
                animator.SetLayerWeight(1, 1f);
            }

            {   //Manage constraints and IKs
                equipData.RightHandIK.data.target = _rightHandIK;
                equipData.LeftHandIK.data.target = _leftHandIK;
            }

            caller.TryGet<RigBuilder>().Build();
            onEquip?.Invoke(equipData);
        }
    }
}