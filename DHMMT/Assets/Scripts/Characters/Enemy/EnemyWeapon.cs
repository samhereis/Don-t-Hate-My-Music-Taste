using Identifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    //Equips weapon on an enemy of awake if "DefaultltWeapon" is not null

    [SerializeField] private InteractableEquipWeapon _defaultltWeapon;
    [SerializeField] private EnemyIdentifier _enemyIdentifier;

    private void OnValidate()
    {
        if (_enemyIdentifier == null) _enemyIdentifier = GetComponent<EnemyIdentifier>();
    }

    private void OnEnable()
    {
        if(_defaultltWeapon != null)
        {
            _defaultltWeapon.Interact(_enemyIdentifier);
        }
    }
}
