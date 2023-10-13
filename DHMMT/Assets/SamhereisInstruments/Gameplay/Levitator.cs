using DG.Tweening;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public sealed class Levitator : MonoBehaviour
    {
        [SerializeField] private Vector3Data _rotationData = new Vector3Data();
        [SerializeField] private Vector3Data _positionData = new Vector3Data();
        [SerializeField] private Vector3Data _scaleData = new Vector3Data();

        [Header("Settings")]
        [SerializeField] private bool _rotationEnable = true;
        [SerializeField] private bool _positionEnable = true;
        [SerializeField] private bool _scaleEnable = true;

        [Header("Debug")]
        [SerializeField] private bool _rotationReached = false;
        [SerializeField] private bool _positionReached = false;
        [SerializeField] private bool _scaleReached = false;


        private void OnEnable()
        {
            _rotationReached = true;
            _positionReached = true;
            _scaleReached = true;

            _rotationData.originalValue = transform.localEulerAngles;
            _positionData.originalValue = transform.localPosition;
            _scaleData.originalValue = transform.localScale;
        }

        private void OnDisable()
        {
            transform.DOKill();
        }

        private void Update()
        {
            if (_rotationEnable) { Rotate(); }
            if (_positionEnable) { Position(); }
            if (_scaleReached) { Scale(); }
        }

        private void Rotate()
        {
            if (_rotationReached)
            {
                _rotationReached = false;

                _rotationData.currentDuration = Random.Range(_rotationData.duration.x, _rotationData.duration.y);
                _rotationData.currentTargetValue = _rotationData.GetRandomValue();

                transform.DOLocalRotate(_rotationData.originalValue + _rotationData.currentTargetValue, _rotationData.currentDuration)
                    .SetEase(Ease.InOutBack).OnComplete(() => _rotationReached = true);
            }
        }

        private void Position()
        {
            if (_positionReached)
            {
                _positionReached = false;

                _positionData.currentDuration = Random.Range(_positionData.duration.x, _positionData.duration.y);
                _positionData.currentTargetValue = _positionData.GetRandomValue();

                var target = _positionData.currentTargetValue;

                transform.DOLocalMove(_positionData.originalValue + target, _positionData.currentDuration).SetEase(Ease.InOutBack).OnComplete(() => _positionReached = true);
            }
        }

        private void Scale()
        {
            if (_scaleReached)
            {
                _scaleReached = false;

                _scaleData.currentDuration = Random.Range(_scaleData.duration.x, _scaleData.duration.y);
                _scaleData.currentTargetValue = _scaleData.GetRandomValue();

                var target = _scaleData.currentTargetValue;

                transform.DOScale(_scaleData.originalValue + target, _scaleData.currentDuration).SetEase(Ease.InOutBack).OnComplete(() => _scaleReached = true);
            }
        }

        [Serializable]
        internal class Vector3Data
        {
            [field: SerializeField] public Vector2 duration = new Vector2(3, 10);

            [Space(5)]
            [field: SerializeField] public Vector2 valueRandomX = new Vector2(-1, 1);
            [field: SerializeField] public Vector2 valueRandomY = new Vector2(-1, 1);
            [field: SerializeField] public Vector2 valueRandomZ = new Vector2(-1, 1);

            [Header("Debug")]
            [field: SerializeField] public Vector3 originalValue;
            [field: SerializeField] public Vector3 currentTargetValue;
            [field: SerializeField] public float currentDuration = 0;

            public Vector3 GetRandomValue()
            {
                float x = Random.Range(valueRandomX.x, valueRandomX.y);
                float y = Random.Range(valueRandomY.x, valueRandomY.y);
                float z = Random.Range(valueRandomZ.x, valueRandomZ.y);

                return new Vector3(x, y, z);
            }
        }
    }
}