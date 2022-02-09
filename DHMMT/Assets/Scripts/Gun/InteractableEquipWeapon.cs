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
    public Transform rightHandIK;
    public Transform leftHandIK;

    [Header("Components")]
    [SerializeField] private Animator _animatorComponent;
    [SerializeField] private Rigidbody _rigidbodyComponent;
    [SerializeField] private Collider _colliderComponent;

    [Header("Events")]
    public readonly UnityEvent<HumanoidData> onEquip = new UnityEvent<HumanoidData>();
    public readonly UnityEvent<HumanoidData> onUnequip = new UnityEvent<HumanoidData>();

    public void Interact(GameObject caller) // in other words - equiod this weapon to the humanoid
    {
        HumanoidData equipData = caller.GetComponent<HumanoidData>();
        Animator animator = caller.GetComponent<Animator>();

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
            equipData.RightHandIK.data.target = rightHandIK;
            equipData.LeftHandIK.data.target = leftHandIK;
        }

        caller.GetComponent<RigBuilder>().Build();

        onEquip?.Invoke(equipData);
    }

    private void setParentConstraint(Transform[] source)
    {
        var data = GetComponent<MultiParentConstraint>().data.sourceObjects;
        data.Clear();
        foreach (Transform obj in source)
        {
            data.Add(new WeightedTransform(obj, 0.05f));
        }
        GetComponent<MultiParentConstraint>().data.sourceObjects = data;
        GetComponent<MultiParentConstraint>().data.constrainedObject = this.transform;
    }

    private void clearParentConstraint()
    {
        GetComponent<MultiParentConstraint>().data.sourceObjects.Clear();
        GetComponent<MultiParentConstraint>().data.constrainedObject = null;
    }

    private void setAimConstraint(Transform source)
    {
        var data = GetComponent<MultiAimConstraint>().data.sourceObjects;
        data.Clear();
        data.Add(new WeightedTransform(source, 1));
        GetComponent<MultiAimConstraint>().data.sourceObjects = data;
        GetComponent<MultiAimConstraint>().data.constrainedObject = this.transform;
    }

    private void clearAimConstraint()
    {
        GetComponent<MultiAimConstraint>().data.sourceObjects.Clear();
        GetComponent<MultiAimConstraint>().data.constrainedObject = null;
    }
}
