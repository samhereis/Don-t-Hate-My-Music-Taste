using System;
using Helpers;
using UnityEngine;

namespace Music
{
    [DisallowMultipleComponent]
    public class MusicReactor_ScaleUI : MonoBehaviour
    {
        private Action _do;

        private enum Axis { X, Y }

        private float _value => _minValue + (_aFrequancyData.value * _aFrequancyData.defaultMultiplierUI);

        [SerializeField] private AFrequancyData _aFrequancyData;

        [Header("Settings")]
        [SerializeField] private float _smoothness = 0.03f;
        [SerializeField] private float _minValue = 1;
        [SerializeField] private Axis _axis;

        private void OnEnable()
        {
            if (_axis == Axis.X)
            {
                _do = () => { transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(_value, transform.localScale.y, transform.localScale.z), _smoothness); };
            }
            else if (_axis == Axis.Y)
            {
                _do = () => { transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, _value, transform.localScale.z), _smoothness); };
            }
        }

        private void Update()
        {
            _do();
        }
    }
}