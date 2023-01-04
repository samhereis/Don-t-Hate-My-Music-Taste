using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _sliderComponent;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _fill;
        [SerializeField] private Image _hurtEffect;

        private void Awake()
        {
            _sliderComponent ??= GetComponent<Slider>();
        }

        public void SetValue(float value)
        {
            _sliderComponent.value = value;
            _fill.color = _gradient.Evaluate(_sliderComponent.normalizedValue);
            _hurtEffect.color = new Color(1, 1, 1, (1 - _sliderComponent.normalizedValue));
        }
    }
}