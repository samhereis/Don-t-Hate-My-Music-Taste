using System;
using UnityEngine;

namespace Music
{
    public class MusicReactor_Scale : MonoBehaviour
    {
        private Action _do;

        private enum Axis { X, Y, Z }


        [SerializeField] private AFrequancyData _aFrequancyData;

        [Header("Settings")]
        [SerializeField] private float _smoothness = 0.25f;
        [SerializeField] private float _minValue = 1;
        [SerializeField] private float _multiplier = 1;
        [SerializeField] private bool _useDefaultMultiplier = true;
        [SerializeField] private Axis _axis;

        private float _currentMultiplier = 5;

        private float _value => _minValue + (_aFrequancyData.value * _currentMultiplier);

        private void Awake()
        {
            UpdateMultiplierValue();
        }

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
            else
            {
                _do = () => { transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, transform.localScale.y, _value), _smoothness); };
            }
        }

        private void OnDisable()
        {
            transform.localScale = Vector3.one;
        }

        private void Update()
        {

#if UNITY_EDITOR

            UpdateMultiplierValue();
#endif

            _do();
        }

        private void UpdateMultiplierValue()
        {
            if (_useDefaultMultiplier) { _currentMultiplier = _aFrequancyData.defaultMultiplier; }
            else { _currentMultiplier = _multiplier; }
        }
    }
}