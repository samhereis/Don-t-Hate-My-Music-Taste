using Identifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GunUse currentWeapon => _currentWeapon;

    [SerializeField] private GunUse _currentWeapon;
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
            _currentWeapon = _defaultltWeapon.GetComponent<GunUse>();
        }
    }
}
