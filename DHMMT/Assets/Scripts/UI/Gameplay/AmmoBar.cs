using Scriptables.Values;
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

    [SerializeField] private IntValue_SO _currentPlayerAmmo;

    private void Awake()
    {
        _sliderComponent ??= GetComponent<Slider>();
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
        _sliderComponent.value = _currentPlayerAmmo.value;
        _fill.color = _gradientColor.Evaluate(_sliderComponent.normalizedValue);
    }
}
