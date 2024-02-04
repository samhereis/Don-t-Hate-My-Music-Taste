using Helpers;
using Interfaces;
using System;
using UnityEngine;

namespace Gameplay.Bullets
{
    public class SimpleGunBullet : ProjectileBase
    {
        public Action onCollided;

        [Header("Components")]
        [SerializeField] private ProjectileView _projectileView;
        [SerializeField] private Collider _collider;

        [Header("Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _selfPutinToPoolTime = 3;
        [SerializeField] private float _putinDelay = 1;

        private void Awake()
        {
            if (_projectileView == null) _projectileView = GetComponentInChildren<ProjectileView>(true);
        }

        private async void OnEnable()
        {
            _projectileView?.Init();
            onCollided += OnEnd;

            await AsyncHelper.DelayFloat(_selfPutinToPoolTime);
            OnEnd();
        }

        private void OnDisable()
        {
            ResetVelocity();

            onCollided -= OnEnd;
            _projectileView?.OnEnd();
        }

        private void Update()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagerActor damagerActor) && damagerActor == this.damagerActor)
            {
                return;
            }

            if (other.TryGetComponent(out IDamagable damagable))
            {
                onCollided?.Invoke();
                Damage(damagable, _damage);
            }
        }

        private async void OnEnd()
        {
            onCollided -= OnEnd;

            await AsyncHelper.DelayFloat(_putinDelay);
            _pooling.PutIn(this);
        }

        private void ResetVelocity()
        {
            transform.position = Vector3.zero;
        }

        public override void Damage(IDamagable damagable, float damage)
        {
            damagable.TakeDamage(_damage, this);
        }
    }
}