using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColloderTakeDamage : MonoBehaviour
{
    public float damage;

    void OnTriggerStay(Collider collision)
    {
        collision.gameObject.GetComponent<IHealthData>()?.TakeDamage(damage);
    }
}
