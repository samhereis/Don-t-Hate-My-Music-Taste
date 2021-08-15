using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    [SerializeField] Gradient gradient;
    [SerializeField] Image Fill;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref slider, GetComponent<Slider>());
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
        if(PlayerWeaponDataHolder.instance == null || PlayerWeaponDataHolder.instance.gunUse == null)
        {
            return;
        }

        slider.maxValue = PlayerWeaponDataHolder.instance.gunUse.maxAmmo;
        slider.value = PlayerWeaponDataHolder.instance.gunUse.CurrentAmmo;
        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
