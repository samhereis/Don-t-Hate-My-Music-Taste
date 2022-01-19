using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHead : MonoBehaviour
{
    // Fire bubble head enemie's interaction with main player

    [SerializeField] private Vector3 _velocity;

    [SerializeField] private float _minSpeed = 2f, _maxSpeed = 4f;
    [SerializeField] private float _damage = 20f;

    private PlayerHealthData _player;

    private float _speed;

    void Awake()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    void FixedUpdate()
    {
        if(CameraMovement.instance != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, CameraMovement.instance.transform.position, ref _velocity, _speed);
            transform.LookAt(CameraMovement.instance.transform);
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        _player = other?.GetComponent<PlayerHealthData>();

        if(_player != null)
        {
            StartCoroutine(Damage());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerHealthData>() != null)
        {
            StopAllCoroutines();

            _player = null;
        }
    }

    IEnumerator Damage()
    {
        while(_player != null)
        {
            _player.TakeDamage(_damage);

            yield return Wait.NewWait(1);
        }
    }
}
