using Helpers;
using Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BulletPooling_SO _pooling;

    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _angleDifference;

    [Header("Debug")]
    [SerializeField] private Vector3 _angle;

    private void OnEnable()
    {
        _angle = new Vector3(Random.Range(-_angleDifference, _angleDifference), Random.Range(-_angleDifference, _angleDifference), _speed);

        _rigidbody.AddRelativeForce(_angle);

        _pooling.PutIn(this, 3);
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;

        transform.position = Vector3.zero;
    } 
}