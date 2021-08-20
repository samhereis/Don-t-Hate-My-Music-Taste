using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleFloor : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IHealthData>() != null)
        {
            other.GetComponent<IHealthData>().TakeDamage(500);
        }
    }
}
