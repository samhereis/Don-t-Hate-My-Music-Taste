using DI;
using System.Collections.Generic;
using UnityEngine;

namespace Music
{
    public class MusicInitializer : MonoBehaviour, IDIDependent
    {
        [SerializeField] private List<SpectrumData> _spectrumData;
        [SerializeField] private List<MusicList_SO> _musicLists;

        [Header("Frequencies")]
        [SerializeField] private List<AFrequancyData> _listOfAllFrequencies = new List<AFrequancyData>();

        private void Awake()
        {
            foreach (var item in _spectrumData)
            {
                item.Initialize();
            }

            foreach (var item in _musicLists)
            {
                item.Initialize();
            }

            foreach (var item in _listOfAllFrequencies)
            {
                item.Initialize();
            }
        }
    }
}