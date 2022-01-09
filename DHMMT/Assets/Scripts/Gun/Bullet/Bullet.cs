using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float  _speed;
    [SerializeField] private float _angleDifference;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private Vector3 _angle;

    private void Awake()
    {
        _angle = new Vector3(Random.Range(-_angleDifference, _angleDifference), Random.Range(-_angleDifference, _angleDifference), _speed);
        Destroy(gameObject, 3);
    }

    void Update()
    {
        _rigidbody.AddRelativeForce(_angle);
    }
}
