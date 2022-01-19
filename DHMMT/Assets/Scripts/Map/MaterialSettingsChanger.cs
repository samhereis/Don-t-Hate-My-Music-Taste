using Scriptables.Holders.Music;
using UnityEngine;

public class MaterialSettingsChanger : MonoBehaviour
{
    // Changes a materials property based on playing music

    [SerializeField] private SpectrumData _spectrumData;

    [Header("What to Change")]
    [SerializeField] private Material _changedMaterial;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private string _settingsName;

    [Header("Indexes")] [SerializeField] private int _start, _end;
    [Header("Value Changers")] [SerializeField] private float _min, _mult;

    private float _value;

    private void Awake()
    {
        _meshRenderer ??= GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        ChanageIntencity();
    }

    private void ChanageIntencity()
    {
        _value = _spectrumData.GetData(_start, _end, _mult, _min);

        _meshRenderer.material.SetFloat(_settingsName, _value);
    }
}
