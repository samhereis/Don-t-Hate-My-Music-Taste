using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleFloor : MonoBehaviour
{
    // Kills a humanoid if it falls

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IHealthData>() != null)
        {
            other.GetComponent<IHealthData>().TakeDamage(500);
        }
    }
}
