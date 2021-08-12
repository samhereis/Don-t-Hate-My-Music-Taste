using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunData : MonoBehaviour
{
    [Header("PreSettings")]
    public Vector3 initialLocalPosition;
    public Vector3 aimPosition;

    [Header("Inverse Kinematics Tramsform")]
    public Transform rightHandIK;
    public Transform leftHandIK;

    [Header("Additional Components")]
    public Animator animator;
}
