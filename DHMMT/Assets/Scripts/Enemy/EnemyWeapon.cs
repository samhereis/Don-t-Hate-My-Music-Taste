using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    //Equips weapon on an enemy of awake if "DefaultltWeapon" is not null

    public InteractableEquipWeapon DefaultltWeapon;

    void OnEnable()
    {
        if(DefaultltWeapon != null)
        {
            DefaultltWeapon.GetComponent<GunUse>().FireRate = GetComponent<EnemyStates>().ShootRate;

            DefaultltWeapon.Interact(gameObject);
        }
    }
}
