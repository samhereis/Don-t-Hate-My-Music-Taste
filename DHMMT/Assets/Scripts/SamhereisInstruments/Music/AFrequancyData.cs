using UnityEngine;

namespace Scriptables.Holders.Music
{
    [CreateAssetMenu(fileName = "A Frequancy Data", menuName = "Scriptables/Music/A Frequancy Data")]
    public class AFrequancyData : ScriptableObject
    {
        [SerializeField] private SpectrumData _playingMusicFrequencies;

        [field: SerializeField] public float value { get; private set; }
        public float valueWithDefaultMultiplier => value * defaultMultiplier;

        [Header("Multiplier")]
        [SerializeField] private float _multiplier = 1;
        [field: SerializeField] public float defaultMultiplier { get; private set; } = 1;

        [Header("Frequency Ranges")]
        [SerializeField] private int _rangeStart = 1;
        [SerializeField] private int _rangeEnd = 5;

        private void OnEnable()
        {
            _playingMusicFrequencies.onValueChanged += GetData;
        }

        public float GetDatWithDefaultMultiplier() => value * defaultMultiplier;

        private async void GetData(float[] spectrumData)
        {
            value = await _playingMusicFrequencies.GetDataAsync(_rangeStart, _rangeEnd, _multiplier);
        }
    }
}