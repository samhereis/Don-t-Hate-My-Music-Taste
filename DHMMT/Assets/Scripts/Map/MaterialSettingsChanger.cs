using Scriptables.Holders.Music;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSettingsChanger : MonoBehaviour
{
    // Changes a materials property based on playing music

    [SerializeField] private SpectrumData _spectrumData;

    [Header("What to Change")]
    [SerializeField] private Material _changedMaterial;
    [SerializeField] private string _settingsName;

    [Header("Indexes")] [SerializeField] private int _start, _end;
    [Header("Value Changers")] [SerializeField] private float _min, _mult;

    private float _value;

    private async void Update()
    {
        _value = await _spectrumData.SetData(_start, _end, _mult, _min);

        _changedMaterial.SetFloat(_settingsName, _value);
    }
}
