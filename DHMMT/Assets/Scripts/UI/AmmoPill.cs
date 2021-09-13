using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPill : MonoBehaviour
{
    // Ammo loot after an enemy dies

    private Vector3 velocity;

    public PlayerWeaponDataHolder Target;

    public float PlusToHealth = 40;

    [SerializeField] private float _speed = 0.5f;

    private void OnEnable()
    {
        Target = PlayerWeaponDataHolder.instance;
    }

    private void FixedUpdate()
    {
        if (PlayerHealthData.instance == null) return;

        transform.position = Vector3.SmoothDamp(transform.position, CameraMovement.instance.transform.position, ref velocity, _speed);
    }

    private void OnTriggerEnter(Collider _triggerEnteredObject_)
    {
        if (_triggerEnteredObject_.GetComponent<PlayerWeaponDataHolder>())
        {
            if (Target.GunUseComponent.MaxAmmo >= Target.GunUseComponent.CurrentAmmo + Target.GunUseComponent.CurrentAmmo/2)
            {
                Target.GunUseComponent.CurrentAmmo = Target.GunUseComponent.MaxAmmo;
            }
            else
            {
                Target.GunUseComponent.CurrentAmmo += Target.GunUseComponent.CurrentAmmo / 3;
            }
            Destroy(gameObject);
        }
    }
}
