using System;
using System.Collections.Generic;
using UnityEngine;

namespace Music
{
    public class MusicVisualizer : MonoBehaviour
    {
        private Action<Transform, int> _do;

        private enum Axis { X, Y, Z }

        [SerializeField] private SpectrumData spectrumData;
        [SerializeField] private Transform _prefab;
        [SerializeField] private Transform _parent;

        [Header("Settings")]
        [SerializeField] private Axis _axis;
        [SerializeField] private float _multiplier = 1f;
        [SerializeField] private float _indexMultiplier = 1f;
        [SerializeField] private float _spawnCount = 1f;
        [SerializeField] private float _smoothness = 0.03f;
        [SerializeField] private float _minValue = 1;
        [SerializeField] private bool _useDefaultMultiplier;

        private List<Transform> _spawnedObjects = new List<Transform>();

        private void Awake()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                _spawnedObjects.Add(Instantiate(_prefab, _parent));
            }
        }

        private void OnEnable()
        {
            if (_axis == Axis.X)
            {
                _do = ScaleX;
            }
            else if (_axis == Axis.Y)
            {
                _do = ScaleY;
            }
            else
            {
                _do = ScaleZ;
            }
        }

        private void Update()
        {
            int index = 1;

            foreach (var spawnedObject in _spawnedObjects)
            {
                _do(spawnedObject, index);
                index++;
            }
        }

        private void ScaleX(Transform scalable, int index)
        {
            scalable.localScale = Vector3.Lerp(scalable.localScale, new Vector3(GetValue(index), scalable.localScale.y, scalable.localScale.z), _smoothness);
        }

        private void ScaleY(Transform scalable, int index)
        {
            scalable.localScale = Vector3.Lerp(scalable.localScale, new Vector3(scalable.localScale.x, GetValue(index), scalable.localScale.z), _smoothness);
        }

        private void ScaleZ(Transform scalable, int index)
        {
            scalable.localScale = Vector3.Lerp(scalable.localScale, new Vector3(scalable.localScale.x, scalable.localScale.y, GetValue(index)), _smoothness);
        }

        private float GetValue(int index)
        {
            return _minValue + (spectrumData.frequencies[index] * _multiplier) * index * _indexMultiplier;
        }
    }
}