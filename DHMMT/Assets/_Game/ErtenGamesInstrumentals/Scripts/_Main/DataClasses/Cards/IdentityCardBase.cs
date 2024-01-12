using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace IdentityCards
{
    [Serializable]
    public class IdentityCardBase<T>
    {
        [field: FoldoutGroup("Base"), SerializeField] public string targetName { get; protected set; }
        [field: FoldoutGroup("Base"), SerializeField] public T target { get; protected set; }

        public IdentityCardBase(T target)
        {
            SetTarget(target);
        }

        public virtual void SetTarget(T target, bool autoSetTargetName = true)
        {
            this.target = target;
            if (autoSetTargetName == true) Validate();
        }

        public virtual void SetTargetName(string targetName)
        {
            this.targetName = targetName;
        }

        [Button]
        public virtual void Validate()
        {
            if (this.target != null)
            {
                SetTargetName(target.ToString());
            }
        }
    }
}