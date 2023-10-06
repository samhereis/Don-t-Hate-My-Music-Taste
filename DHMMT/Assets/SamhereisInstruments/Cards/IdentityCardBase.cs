using Helpers;
using UnityEngine;

namespace IdentityCards
{
    public abstract class IdentityCardBase<T> : ScriptableObject
    {
        [field: SerializeField] public T target { get; protected set; }
        [field: SerializeField] public string targetName { get; protected set; }

        private void OnValidate()
        {
            if (targetName != name)
            {
                targetName = name;
                this.TrySetDirty();
            }
        }
    }
}