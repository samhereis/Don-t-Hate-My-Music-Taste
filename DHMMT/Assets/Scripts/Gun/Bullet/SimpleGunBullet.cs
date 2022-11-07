using Characters.States.Data;
using Helpers;
using Interfaces;
using Mirror;
using Network;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Bullets
{
    public class SimpleGunBullet : ProjectileBase
    {
        public Action onCollided;

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

            _angle = new Vector3(Random.Range(-_angleDifference, _angleDifference), Random.Range(-_angleDifference, _angleDifference), _speed * 100);
            onCollided += OnEnd;

            _rigidbody.AddRelativeForce(_angle);

            await AsyncHelper.DelayAndDo(_selfPutinToPoolTime, () => OnEnd());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                if (other.TryGetComponent(out NetworkIdentity network))
                {
                    DamagePlayer(network.netId.ToString(), _damage);
                }

                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
                _rigidbody.ResetInertiaTensor();

                onCollided?.Invoke();
            }
        }

        [ClientRpc]
        private async void DamagePlayer(string netID, float damage)
        {
            var player = await PlayersContainer.GetPlayer(netID);

            if(player.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.TakeDamage(damage);
            }
        }

        private void OnEnd()
        {
            onCollided -= OnEnd;

            _projectileView?.OnEnd(() =>
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.ResetInertiaTensor();

                transform.position = Vector3.zero;

                _pooling.PutIn(this);
            });
        }
    }
}