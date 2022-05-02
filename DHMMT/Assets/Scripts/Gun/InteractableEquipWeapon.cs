using Identifiers;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class InteractableEquipWeapon : MonoBehaviour, IInteractable
{
    // Controlls how a weapon equips

    [SerializeField] private bool _equiped = false;
    public bool isInteractable => !_equiped;

    [SerializeField] private string _itemName = "Head Swaper";

    public string ItemName { get { return _itemName; } private set { _itemName = value; } }

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

    public void Interact(IdentifierBase caller) // in other words - equiod this weapon to the humanoid
    {
        HumanoidData equipData = caller.TryGet<HumanoidData>();
        Animator animator = caller.TryGet<Animator>();

        _equiped = true;

        {   //Manage Position and Rotation
            transform.SetParent(equipData.weapnHolder, false);
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
