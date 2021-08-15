using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    public Slider slider;

    [SerializeField] Gradient gradient;
    [SerializeField] Image Fill;
    [SerializeField] Image HurtEffect;

    private void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        ExtentionMethods.SetWithNullCheck(ref slider, GetComponent<Slider>());
    }

    public void SetValue(float value)
    {
        slider.value = value;
        Fill.color = gradient.Evaluate(slider.normalizedValue);
        HurtEffect.color = new Color(1, 1, 1, (1 - slider.normalizedValue));
    }
}
