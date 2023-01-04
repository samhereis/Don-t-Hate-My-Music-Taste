using Characters.States.Data;
using Helpers;
using Interfaces;
using Photon.Pun;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public override async void OnEnable()
        {
            base.OnEnable();

            if (photonView.IsMine)
            {
                gameObject.GetPhotonView().RPC(nameof(RPC_OnStart), RpcTarget.All);

                _angle = new Vector3(Random.Range(-_angleDifference, _angleDifference), Random.Range(-_angleDifference, _angleDifference), _speed * 100);

                _rigidbody.AddRelativeForce(_angle);

                await AsyncHelper.DelayAndDo(_selfPutinToPoolTime, () =>
                {
                    if (gameObject != null && gameObject.activeSelf)
                    {
                        gameObject.GetPhotonView().RPC(nameof(RPC_OnEnd), RpcTarget.All);
                    }
                });
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (photonView.IsMine)
            {
                if (other.TryGetComponent(out IDamagable damagable))
                {
                    other.gameObject.GetPhotonView().RPC(nameof(damagable.RPC_TakeDamage), RpcTarget.All, _damage);
                    gameObject.GetPhotonView().RPC(nameof(RPC_OnEnd), RpcTarget.All);
                }
            }
        }

        [PunRPC]
        private void RPC_OnStart()
        {
            gameObject.gameObject.SetActive(true);
            _projectileView?.Init();
        }

        [PunRPC]
        private async void RPC_OnEnd()
        {
            await _projectileView?.OnEnd();

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.ResetInertiaTensor();

            transform.position = Vector3.zero;

            if (photonView.IsMine)
            {
                _pooling.PutIn(this);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}