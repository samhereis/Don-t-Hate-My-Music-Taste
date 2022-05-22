using Scriptables.Values;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AmmoBar : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Slider _sliderComponent;
        [SerializeField] private Gradient _gradientColor;
        [SerializeField] private Image _fill;

        [Header("SO")]
        [SerializeField] private IntValue_SO _currentPlayerAmmo;

        private void Awake()
        {
            _sliderComponent ??= GetComponent<Slider>();
            _currentPlayerAmmo.AddListener(UpdateAmmo);
        }

        private void OnDestroy()
        {
            _currentPlayerAmmo.RemoveListener(UpdateAmmo);
        }

        private void UpdateAmmo(int value)
        {
            _sliderComponent.value = value;
            _fill.color = _gradientColor.Evaluate(_sliderComponent.normalizedValue);
        }
    }
}