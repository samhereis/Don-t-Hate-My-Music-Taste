using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    // Ammo bar while gameplay

    [SerializeField] private Slider _sliderComponent;

    [SerializeField] private Gradient _gradientColor;
    [SerializeField] private Image _fill;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref _sliderComponent, GetComponent<Slider>());
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(UpdateAmmo), 3, 0.3f);
    }
    private void OnDisable()
    {
        CancelInvoke(nameof(UpdateAmmo));
    }
    void UpdateAmmo()
    {
        if(PlayerWeaponDataHolder.instance == null || PlayerWeaponDataHolder.instance.GunUseComponent == null)
        {
            return;
        }

        _sliderComponent.maxValue = PlayerWeaponDataHolder.instance.GunUseComponent.MaxAmmo;
        _sliderComponent.value = PlayerWeaponDataHolder.instance.GunUseComponent.CurrentAmmo;
        _fill.color = _gradientColor.Evaluate(_sliderComponent.normalizedValue);
    }
}
