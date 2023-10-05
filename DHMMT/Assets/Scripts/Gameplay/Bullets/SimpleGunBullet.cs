using Interfaces;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Bullets
{
    public class SimpleGunBullet : ProjectileBase
    {
        public Quaternion quaternion;

        public Action onCollided;

        [Header("Components")]
        [SerializeField] private ProjectileView _projectileView;
        [SerializeField] private Collider _collider;

        [Header("Settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _selfPutinToPoolTime = 3;

        private void OnValidate()
        {
            if (_projectileView == null) _projectileView = GetComponentInChildren<ProjectileView>();
        }

        private void OnEnable()
        {
            _projectileView?.Init();
            onCollided += OnEnd;

            StartCoroutine(AutoEnd());
        }

        private void OnDisable()
        {
            ResetVelocity();

            onCollided -= OnEnd;
            _projectileView?.OnEnd();
        }

        private void Update()
        {
            quaternion = transform.rotation;
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

        public override void Initialize(IDamagerActor damagerActor)
        {
            base.Initialize(damagerActor);
        }

        private void OnEnd()
        {
            onCollided -= OnEnd;

            _projectileView?.OnEnd(() =>
            {
                _pooling.PutIn(this);
            });
        }

        private IEnumerator AutoEnd()
        {
            yield return new WaitForSeconds(_selfPutinToPoolTime);

            OnEnd();
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