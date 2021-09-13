using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Health bar while gameplay

    public static HealthBar instance;

    public Slider SliderComponent;

    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;
    [SerializeField] private Image _hurtEffect;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        ExtentionMethods.SetWithNullCheck(ref SliderComponent, GetComponent<Slider>());
    }

    public void SetValue(float value)
    {
        SliderComponent.value = value;
        _fill.color = _gradient.Evaluate(SliderComponent.normalizedValue);
        _hurtEffect.color = new Color(1, 1, 1, (1 - SliderComponent.normalizedValue));
    }
}
