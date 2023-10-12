using SO;
using System;
using UnityEngine;

namespace Sound
{
    [Serializable]
    public class SimpleSound : SoundBase
    {
        [field: SerializeField] public AString soundName { get; private set; }
    }
}