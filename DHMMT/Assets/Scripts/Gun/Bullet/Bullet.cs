using Helpers;
using Interfaces;
using Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BulletPooling_SO _pooling;
    [SerializeField] private TrailRenderer _trailRenderer;

    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _angleDifference;
    [SerializeField] private float _damage;

    [Header("Debug")]
    [SerializeField] private Vector3 _angle;

    private void OnEnable()
    {
        _trailRenderer.time = 0.01f;

        _angle = new Vector3(Random.Range(-_angleDifference, _angleDifference), Random.Range(-_angleDifference, _angleDifference), _speed);

        _rigidbody.AddRelativeForce(_angle);

        _pooling.PutIn(this, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);

            Debug.Log(damagable);
        }
    }

    private void OnDisable()
    {
        _trailRenderer.time = 0;

        _rigidbody.velocity = Vector3.zero;

        transform.position = Vector3.zero;
    } 
}