using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPill : MonoBehaviour
{
    Vector3 velocity;
    public PlayerWeaponDataHolder target;
    public float PlusToHealth = 40;
    [SerializeField] float speed = 0.5f;
    private void OnEnable()
    {
        target = PlayerWeaponDataHolder.instance;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, CameraMovement.instance.transform.position, ref velocity, speed);
    }
    void OnTriggerEnter(Collider _triggerEnteredObject_)
    {
        if (_triggerEnteredObject_.GetComponent<PlayerWeaponDataHolder>())
        {
            if (target.gunUse.maxAmmo >= target.gunUse.CurrentAmmo + target.gunUse.CurrentAmmo/2)
            {
                target.gunUse.CurrentAmmo = target.gunUse.maxAmmo;
            }
            else
            {
                target.gunUse.CurrentAmmo += target.gunUse.CurrentAmmo / 2;
            }
            Destroy(gameObject);
        }
    }
}
