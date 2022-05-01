using Helpers;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Bullets
{
    public class SimpleGunBullet : ProjectileBase
    {
        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ProjectileView _projectileView;

        [Header("Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _angleDifference;
        [SerializeField] private float _damage;
        [SerializeField] private float _selfPutinToPoolTime = 3;

        [Header("Debug")]
        [SerializeField] private Vector3 _angle;

        private void OnValidate()
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
            if (_projectileView == null) _projectileView = GetComponentInChildren<ProjectileView>();
        }

        private async void OnEnable()
        {
            _projectileView?.Init();

            _angle = new Vector3(Random.Range(-_angleDifference, _angleDifference), Random.Range(-_angleDifference, _angleDifference), _speed);

            _rigidbody.AddRelativeForce(_angle);

            await AsyncHelper.Delay(_selfPutinToPoolTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_damage);

                Debug.Log(damagable);
            }
        }

        private void OnDisable()
        {
            _projectileView?.OnEnd(() =>
            {
                _pooling.PutIn(this, 0.25f);

                _rigidbody.velocity = Vector3.zero;
                transform.position = Vector3.zero;
            });
        }
    }
}