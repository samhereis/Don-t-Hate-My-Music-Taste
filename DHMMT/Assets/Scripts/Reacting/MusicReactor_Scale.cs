using DG.Tweening;
using Scriptables.Holders.Music;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicReactor_Scale : MonoBehaviour
{
    [SerializeField] private AFrequancyData _aFrequancyData;

    [Header("Settings")]
    [SerializeField] private float _smoothness = 1;
    [SerializeField] private float _minValue = 1;

    private enum Axis { X, Y, Z }

    [SerializeField] private Axis _axis;

    private Action Do;

    private void OnEnable()
    {
        if(_axis == Axis.X)
        {
            Do = () => { transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(_minValue + _aFrequancyData.value, transform.localScale.y, transform.localScale.z), _smoothness); };
        }
        else if (_axis == Axis.Y)
        {
            Do = () => { transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, _minValue + _aFrequancyData.value, transform.localScale.z), _smoothness); };
        }
        else
        {
            Do = () => { transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, transform.localScale.y, _minValue + _aFrequancyData.value), _smoothness); };
        }
    }

    private async void Update()
    {
        await ExtentionMethods.Delay();

        Do();
    }
}
