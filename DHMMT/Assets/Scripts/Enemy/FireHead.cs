using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHead : MonoBehaviour
{
    [SerializeField] Vector3 velocity;

    [SerializeField] float Minspeed = 2f, MaxSpeed = 4f;
    float speed;
    [SerializeField] float damage = 20f;

    PlayerHealthData enemy;

    void Awake()
    {
        speed = Random.Range(Minspeed, MaxSpeed);
    }

    void FixedUpdate()
    {
        if(CameraMovement.instance != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, CameraMovement.instance.transform.position, ref velocity, speed);
            transform.LookAt(CameraMovement.instance.transform);
        }    
    }

    void OnTriggerEnter(Collider other)
    {
        enemy = other.GetComponent<PlayerHealthData>();

        StartCoroutine(Damage());
    }

    void OnTriggerExit()
    {
        StopAllCoroutines();

        enemy = null;
    }

    IEnumerator Damage()
    {
        while(enemy != null)
        {
            enemy.TakeDamage(damage);
            yield return Wait.NewWait(1);
        }
    }
}
