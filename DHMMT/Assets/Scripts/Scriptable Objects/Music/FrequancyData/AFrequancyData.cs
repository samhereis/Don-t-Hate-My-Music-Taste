using UnityEngine;

namespace Scriptables.Holders.Music
{
    [CreateAssetMenu(fileName = "A Frequancy Data", menuName = "Scriptables/Music/A Frequancy Data")]
    public class AFrequancyData : ScriptableObject
    {
        [SerializeField] private SpectrumData _playingMusicFrequencies;

        [SerializeField] private float _value;
        public float value => _value;

        [Header("Multiplier")]
        [SerializeField] private float _multiplier = 1;

        [Header("Frequency Ranges")]
        [SerializeField] private int _rangeStart = 1;
        [SerializeField] private int _rangeEnd = 5;

        private void OnEnable()
        {
            _playingMusicFrequencies.onValueChanged += GetData;
        }

        private async void GetData(float[] spectrumData)
        {
            _value = await _playingMusicFrequencies.SetData(_rangeStart, _rangeEnd, _multiplier);
        }
    }
}