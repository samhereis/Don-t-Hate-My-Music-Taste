using Scriptables.Holders.Music;
using UnityEngine;

public class StandartShaderIntesityChanger : MonoBehaviour
{
    // TODO: Refactor this script

    [SerializeField] private SpectrumData _spectrumData;

    [Header("What to change")]
    [SerializeField] private Material _sun;
    [SerializeField] private string _settingsName;

    [Header("indexes")] [SerializeField] private int _start, _end;

    [Header("value changers")] [SerializeField] private float _min, _mult;

    [Header("colors")] [SerializeField] private float _red, _green, _blue;

    [Header("Debug")] private float _value;

    private void Awake()
    {
        _red = _sun.color.r;
        _green = _sun.color.g;
        _blue = _sun.color.b;
    }

    private async void FixedUpdate()
    {
        _value = await _spectrumData.SetData(_start, _end, _mult, _min);

        _sun.SetVector(_settingsName, new Color(_red, _green, _blue) * _value);
    }
}
