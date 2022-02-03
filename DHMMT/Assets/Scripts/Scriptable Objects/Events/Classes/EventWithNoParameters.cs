using Helpers;
using System;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New Event With No Parameter", menuName = "Scriptables/Events/Event With No Parameter")]
    public class EventWithNoParameters : ScriptableObject
    {
        public Action onInvoke { get; private set; }

        public async void AdListener(Action action)
        {
            await AsyncHelper.Delay();

            onInvoke += action;
        }
        public async void RemoveListener(Action action)
        {
            await AsyncHelper.Delay();

            onInvoke -= action;
        }

        public async void Invoke()
        {
            await AsyncHelper.Delay();

            onInvoke?.Invoke();
        }
    }
}