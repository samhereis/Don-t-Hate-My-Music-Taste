using Identifiers;
using UnityEngine;

namespace IdentityCards
{
    [CreateAssetMenu(fileName = "EnemyIdentityCard", menuName = "Scriptables/IdentityCards/EnemyIdentityCard")]
    public class EnemyIdentityCard : ScriptableObject
    {
        [field: SerializeField] public IdentityCardBase<EnemyIdentifier> identityCard;
    }
}