using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPill : MonoBehaviour
{
    // Ammo loot after an enemy dies

    private Vector3 velocity;

    public float PlusToHealth = 40;

    [SerializeField] private float _speed = 0.5f;

    private void OnEnable()
    {

    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider _triggerEnteredObject_)
    {

    }
}
