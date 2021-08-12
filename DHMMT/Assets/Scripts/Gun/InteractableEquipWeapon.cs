using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class InteractableEquipWeapon : MonoBehaviour, IInteractable
{
    public bool equiped = false;

    private string itemName = "Head Swaper";
    public string ItemName { get { return itemName;} set { itemName = value; }}

    public GameObject InteractableObject { get => gameObject; set { } }

    public Type InteractorDataType { get => typeof(EquipWeaponData); set => throw new NotImplementedException(); }

    public GunData gunData;

    private void Awake ()
    {
        ExtentionMethods.SetWithNullCheck(ref gunData, GetComponent<GunData>());
    }

    public void Interact(GameObject caller)
    {
        EquipWeaponData equipData = caller.GetComponent<EquipWeaponData>();
        Animator animator = caller.GetComponent<Animator>();

        equiped = true;

        {   //Manage Position and Rotation
            transform.SetParent(equipData.weaponPosition, false);
            equipData.weaponPosition.localPosition = gameObject.GetComponent<GunData>().initialLocalPosition;
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
            equipData.rightHandIK.data.target = gunData.rightHandIK;
            equipData.leftHandIK.data.target = gunData.leftHandIK;
        }

        {
            caller.GetComponent<WeaponDataHolder>().Set(gunData, GetComponent<GunUse>(), GetComponent<GunAim>());
        }
        caller.GetComponent<RigBuilder>().Build();
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
