using System;
using Helpers;
using UnityEngine;

namespace Music
{
    [DisallowMultipleComponent]
    public class MusicReactor_Scale : MonoBehaviour
    {
        private Action _do;

        private enum Axis { X, Y, Z }

        private float _value => _minValue + (_aFrequancyData.value * _multiplier);

        [SerializeField] private AFrequancyData _aFrequancyData;

        [Header("Settings")]
        [SerializeField] private float _smoothness = 0.03f;
        [SerializeField] private float _minValue = 1;
        [SerializeField] private float _multiplier = 1;
        [SerializeField] private bool _useDefaultMultiplier;
        [SerializeField] private Axis _axis;

        private void OnValidate()
        {
            if (Application.isPlaying) return;

            if (GetComponent<RectTransform>() != null)
            {
                if (GetComponent<MusicReactor_ScaleUI>() == null)
                {
                    gameObject.AddComponent<MusicReactor_ScaleUI>().setData(_aFrequancyData);
                }
                else
                {

                }

                Debug.Log("MusicReactor_Scale Here " + gameObject.name, this);

                this.hideFlags = HideFlags.HideInInspector;
                DestroyImmediate(this);
                Destroy(this);

                this.TrySetDirty();
            }
        }

        private void Awake()
        {
            if (_useDefaultMultiplier) _multiplier = _aFrequancyData.defaultMultiplier;
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

        private void Update()
        {
            _do();
        }
    }
}