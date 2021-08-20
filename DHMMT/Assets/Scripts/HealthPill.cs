using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPill : MonoBehaviour
{
    Vector3 velocity;
    public PlayerHealthData target;
    public float PlusToHealth = 40;
    [SerializeField] float speed = 0.5f;
    private void OnEnable()
    {
        target = PlayerHealthData.instance;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, CameraMovement.instance.transform.position, ref velocity, speed);
    }
    void OnTriggerEnter(Collider _triggerEnteredObject_)
    {
        if(_triggerEnteredObject_.GetComponent<PlayerHealthData>())
        {
            if(target.MaxHealth >= target.Health + PlusToHealth)
            {
                target.Health = target.MaxHealth;
            }
            else
            {
                target.Health += PlusToHealth;
            }
            Destroy(gameObject);
        }
    }
}
