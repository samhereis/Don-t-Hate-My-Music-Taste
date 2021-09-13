using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class InteractableEquipWeapon : MonoBehaviour, IInteractable
{
    // Controlls how a weapon equips

    public bool Equiped = false;

    public bool Interactable { get; set; }

    private string _itemName = "Head Swaper";
    public string ItemName { get { return _itemName;} set { _itemName = value; }}

    public GameObject InteractableObject { get => gameObject; set { } }

    //public Type InteractorDataType { get => typeof(HumanoidEquipWeaponData); set => throw new NotImplementedException(); }

    public GunData GunDataComponent;

    private void Awake ()
    {
        ExtentionMethods.SetWithNullCheck(ref GunDataComponent, GetComponent<GunData>());
    }

    public void Interact(GameObject caller) // in other words - equiod this weapon to the humanoid
    {
        HumanoidEquipWeaponData equipData = caller.GetComponent<HumanoidEquipWeaponData>();
        Animator animator = caller.GetComponent<Animator>();

        Equiped = true;
        Interactable = false;

        {   //Manage Position and Rotation
            transform.SetParent(equipData.WeaponPosition, false);
            equipData.WeaponPosition.localPosition = gameObject.GetComponent<GunData>().initialLocalPosition;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        {   //Manage RigidBody
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<BoxCollider>().enabled = false;
        }

        {   //Manage player animation layer
            animator.SetBool("hands", false);
            animator.SetLayerWeight(1, 1f);
        }

        {   //Manage constraints and IKs
            equipData.RightHandIK.data.target = GunDataComponent.rightHandIK;
            equipData.LeftHandIK.data.target = GunDataComponent.leftHandIK;
        }

        {
            caller.GetComponent<WeaponDataHolder>().Set(GunDataComponent, GetComponent<GunUse>(), GetComponent<GunAim>());
        }
        caller.GetComponent<RigBuilder>().Build();
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void Activate(bool value)
    {
        gameObject.SetActive(value);
    }

    public void MoveTo(Transform parent)
    {
        transform.SetParent(parent);
    }

    void setParentConstraint(Transform[] source)
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

    void clearParentConstraint()
    {
        GetComponent<MultiParentConstraint>().data.sourceObjects.Clear();
        GetComponent<MultiParentConstraint>().data.constrainedObject = null;
    }

    void setAimConstraint(Transform source)
    {
        var data = GetComponent<MultiAimConstraint>().data.sourceObjects;
        data.Clear();
        data.Add(new WeightedTransform(source, 1));
        GetComponent<MultiAimConstraint>().data.sourceObjects = data;
        GetComponent<MultiAimConstraint>().data.constrainedObject = this.transform;
    }

    void clearAimConstraint()
    {
        GetComponent<MultiAimConstraint>().data.sourceObjects.Clear();
        GetComponent<MultiAimConstraint>().data.constrainedObject = null;
    }
}
