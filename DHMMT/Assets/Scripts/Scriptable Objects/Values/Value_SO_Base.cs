using UnityEngine;
using UnityEngine.Events;

namespace Scriptables.Values
{
    public abstract class Value_SO_Base<T> : ScriptableObject
    {
        [SerializeField] protected T _value;
        public abstract T value { get; }

        protected abstract UnityEvent<T> onValueChange { get; }

        public virtual void AddListener(UnityAction<T> listener)
        {
            onValueChange.AddListener(listener);
        }

        public virtual void RemoveListener(UnityAction<T> listener)
        {
            onValueChange.RemoveListener(listener);
        }

        public virtual void ChangeValue(T sentValue)
        {
            _value = sentValue;
            onValueChange.Invoke(sentValue);
        }
    }
}