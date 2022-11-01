using Identifiers;
using UnityEngine;

namespace IdentityCards
{
    [CreateAssetMenu(fileName = "APlayerIdentityCard ", menuName = "Scriptables/IdentityCards/APlayerIdentityCard")]
    public sealed class APlayerIdentityCard : IdentityCardBase<IdentifierBase>
    {
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(targetName)) targetName = this.name;
        }
    }
}