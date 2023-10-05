using DI;
using ConstStrings;
using Interfaces;
using UnityEngine;

namespace Music
{
    [CreateAssetMenu(fileName = "A Frequancy Data", menuName = "Scriptables/Music/A Frequancy Data")]
    public class AFrequancyData : ScriptableObject, IInitializable, IDIDependent
    {
        [field: SerializeField] public float value { get; private set; }
        public float valueWithDefaultMultiplier => value * defaultMultiplier;

        [Header("Multiplier")]
        [SerializeField] private float _multiplier = 1;
        [field: SerializeField] public float defaultMultiplier { get; private set; } = 1;

        [Header("Frequency Ranges")]
        [SerializeField] private int _rangeStart = 1;
        [SerializeField] private int _rangeEnd = 5;

        [Header("SO")]
        [DI(DIStrings.spectrumDataContainer)] [SerializeField] private SpectrumData _playingMusicFrequencies;

        public void Initialize()
        {
            (this as IDIDependent).LoadDependencies();

            _playingMusicFrequencies.onValueChanged += GetData;
        }

        private void GetData(float[] spectrumData)
        {
            value = _playingMusicFrequencies.GetData(_rangeStart, _rangeEnd, _multiplier);
        }
    }
}