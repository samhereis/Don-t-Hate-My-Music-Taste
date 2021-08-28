using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public InteractableEquipWeapon DefaultltWeapon;

    void OnEnable()
    {
        DefaultltWeapon.GetComponent<GunUse>().fireRate = GetComponent<EnemyStates>().ShootRate;

        DefaultltWeapon.Interact(gameObject);
    }
}
