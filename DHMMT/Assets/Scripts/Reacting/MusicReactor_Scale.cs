using DG.Tweening;
using Scriptables.Holders.Music;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicReactor_Scale : MonoBehaviour
{
    [SerializeField] private AFrequancyData _aFrequancyData;

    [Header("Settings")]
    [SerializeField] private float _smoothness = 0.03f;
    [SerializeField] private float _minValue = 1;

    [SerializeField] private float _multiplier = 1;

    [SerializeField] private bool _useDefaultMultiplier;

    [SerializeField] private Axis _axis;

    private enum Axis { X, Y, Z }

    private Action Do;

    private float _value => _minValue + (_aFrequancyData.value * _multiplier);

    private void Awake()
    {
        if (_useDefaultMultiplier) _multiplier = _aFrequancyData.defaultMultiplier;
    }

    private void OnEnable()
    {
        if(_axis == Axis.X)
        {
            Do = () => { transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(_value, transform.localScale.y, transform.localScale.z), _smoothness); };
        }
        else if (_axis == Axis.Y)
        {
            Do = () => { transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, _value, transform.localScale.z), _smoothness); };
        }
        else
        {
            Do = () => { transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, transform.localScale.y, _value), _smoothness); };
        }
    }

    private void Update()
    {
        Do();
    }
}

