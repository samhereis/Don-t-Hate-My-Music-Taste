using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyFloor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerJump>()?.PerformJump(1);
    }

    void OnCollisionEnter(Collision other)
    {
        other.collider.GetComponent<PlayerJump>()?.PerformJump(1);
    }
}
