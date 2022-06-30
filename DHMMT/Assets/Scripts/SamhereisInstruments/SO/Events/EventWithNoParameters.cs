using Samhereis.Helpers;
using System;
using UnityEngine;

namespace Samhereis.Events
{
    [CreateAssetMenu(fileName = "New Event With No Parameter", menuName = "Scriptables/Events/Event With No Parameter")]
    public class EventWithNoParameters : ScriptableObject
    {
        public Action onInvoke { get; private set; }

        public void AdListener(Action action)
        {
            onInvoke += action;
        }

        public void RemoveListener(Action action)
        {
            onInvoke -= action;
        }

        public async void Invoke()
        {
            await AsyncHelper.Delay(() => onInvoke?.Invoke());
        }
    }
}