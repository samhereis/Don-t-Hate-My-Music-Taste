using Scriptables.Holders.Music;
using UnityEngine;

public class intensityChanger : MonoBehaviour
{
    // Changes intencity of a material based on "PlayingMusicData" data

    [SerializeField] private Light _lightObj;

    [Header("Indexes")]
    [SerializeField] private int _start, _end;

    [Header("Values Changers")]
    [SerializeField] private float _min, _mult;

    [SerializeField] private SpectrumData _spectrumData;

    private async void Update()
    {
        _lightObj.intensity = await _spectrumData.SetData(_start, _end, _mult, _min);
    }
}
