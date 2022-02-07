using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    //Equips weapon on an enemy of awake if "DefaultltWeapon" is not null

    [SerializeField] private InteractableEquipWeapon _defaultltWeapon;

    private void OnEnable()
    {
        if(_defaultltWeapon != null)
        {
            _defaultltWeapon.Interact(gameObject);
        }
    }
}
