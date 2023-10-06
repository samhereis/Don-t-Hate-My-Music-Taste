using Interfaces;
using System;
using System.Linq;
using UnityEngine;

namespace Music
{
    [CreateAssetMenu(fileName = "Spectrum Width Holder", menuName = "Scriptables/Music")]
    public class SpectrumData : ScriptableObject, IInitializable
    {
        [field: SerializeField] public float[] frequencies { get; private set; } = new float[64];

        public Action<float[]> onValueChanged;

        public void Initialize()
        {
            onValueChanged = null;
        }

        public float[] SetSpectrumWidth(AudioSource audioSource)
        {
            audioSource.GetSpectrumData(frequencies, 0, FFTWindow.Blackman);

            onValueChanged?.Invoke(frequencies);

            return frequencies;
        }

        public float GetData(int start, int end, float multiplier)
        {
            return frequencies[start..end].Average() * multiplier;
        }

        public float GetData(int start, int end, float multiplier, float minValue)
        {
            return minValue + frequencies[start..end].Average() * multiplier;
        }
    }
}