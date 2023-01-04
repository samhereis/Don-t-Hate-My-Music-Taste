using Identifiers;
using UnityEngine;

namespace Gameplay
{
    public class EnemyWeapon : MonoBehaviour
    {
        [field: SerializeField] public GunUse currentWeapon { get; private set; }
        [SerializeField] private InteractableEquipWeapon _defaultltWeapon;
        [SerializeField] private EnemyIdentifier _enemyIdentifier;

        private void OnValidate()
        {
            if (_enemyIdentifier == null) _enemyIdentifier = GetComponent<EnemyIdentifier>();
        }

        private void OnEnable()
        {
            if (_defaultltWeapon != null)
            {
                _defaultltWeapon.Interact(_enemyIdentifier);
                currentWeapon = _defaultltWeapon.GetComponent<GunUse>();
            }
        }
    }
}