using Helpers;
using System;
using UnityEngine;

namespace Events
{
    public class EventWithOneParameterBase<T> : ScriptableObject
    {
        public Action<T> onInvoke { get; private set; }

        public virtual void AdListener(Action<T> action)
        {
            onInvoke += action;
        }

        public virtual void RemoveListener(Action<T> action)
        {
            onInvoke -= action;
        }

        public virtual async void Invoke(T parameter)
        {
            await AsyncHelper.Delay(() => onInvoke?.Invoke(parameter));
        }
    }
}