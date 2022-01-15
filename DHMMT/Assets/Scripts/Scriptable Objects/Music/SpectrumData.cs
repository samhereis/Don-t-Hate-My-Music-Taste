using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Scriptables.Holders.Music
{
    [CreateAssetMenu(fileName = "Spectrum Width Holder", menuName = "Scriptables/Music")]
    public class SpectrumData : ScriptableObject
    {
        [SerializeField] private float[] _frequencies = new float[64];
        public float[] frequencies => _frequencies;

        public Action<float[]> onValueChanged;

        public float[] SetSpectrumWidth(AudioSource audioSource)
        {
            audioSource.GetSpectrumData(_frequencies, 0, FFTWindow.Blackman);

            onValueChanged?.Invoke(_frequencies);

            return _frequencies;
        }

        public async Task<float> SetData(int start, int end, float multiplier)
        {
            await ExtentionMethods.Delay();

            return frequencies[start..end].Average() * start * start * multiplier;
        }

        public async Task<float> SetData(int start, int end, float multiplier, float minValue)
        {
            await ExtentionMethods.Delay();

            return minValue + frequencies[start..end].Average() * start * start * multiplier;
        }
    }
}