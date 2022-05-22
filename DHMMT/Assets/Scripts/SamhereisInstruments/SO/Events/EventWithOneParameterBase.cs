using Helpers;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class EventWithOneParameterBase<T> : ScriptableObject
    {
        public Action<T> onInvoke { get; private set; }

        public virtual async void AdListener(Action<T> action)
        {
            await AsyncHelper.Delay(() => onInvoke += action);
        }
        public virtual async void RemoveListener(Action<T> action)
        {
            await AsyncHelper.Delay(() => onInvoke -= action);
        }

        public virtual async void Invoke(T parameter)
        {
            await AsyncHelper.Delay(() => onInvoke?.Invoke(parameter));
        }
    }
}