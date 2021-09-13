using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColloderTakeDamage : MonoBehaviour
{
    // Damage humadoid when it touches this objecs

    public float Damage;

    public void OnTriggerStay(Collider collision)
    {
        collision.gameObject.GetComponent<IHealthData>()?.TakeDamage(Damage);
    }
}
