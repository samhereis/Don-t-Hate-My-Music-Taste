using System;
using UnityEngine;

namespace Sound
{
    [Serializable]
    public class EventBasedSound : SoundBase
    {
        [field: SerializeField] public string eventName { get; private set; }
    }
}