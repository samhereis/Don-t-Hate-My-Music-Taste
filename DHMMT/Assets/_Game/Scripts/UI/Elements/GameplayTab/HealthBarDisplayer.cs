using ConstStrings;
using DataClasses;
using DependencyInjection;
using DG.Tweening;
using Observables;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements.GameplayTab
{
    public class HealthBarDisplayer : MonoBehaviour, INeedDependencyInjection
    {
        [Header("Components")]
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Image _healthBarFill;

        [Header("Settings")]
        [SerializeField] private Gradient _healthBarGradient;

        [Inject(ObservableValue_ConstStrings.playerHealth)] private ObservableValue<PlayerHealthData> _playerHealthValue;

        private void Awake()
        {
            _healthBar = GetComponentInChildren<Slider>(true);
            _healthBarFill = _healthBar.fillRect.GetComponent<Image>();

            DependencyContext.diBox.InjectDataTo(this);
        }

        private void OnEnable()
        {
            _playerHealthValue.AddListener(OnPlayerHealthValueChanged);

            OnPlayerHealthValueChanged(_playerHealthValue.value);
        }

        private void OnDisable()
        {
            _playerHealthValue.RemoveListener(OnPlayerHealthValueChanged);
        }

        private void OnPlayerHealthValueChanged(PlayerHealthData damageData)
        {
            if (_healthBar != null)
            {
                _healthBar.maxValue = damageData.maxHealth;

                _healthBar.DOKill();
                _healthBar.DOValue(damageData.healthAfter, 0.5f);
            }

            if (_healthBarFill != null)
            {
                _healthBarFill.DOKill();
                _healthBarFill.DOColor(_healthBarGradient.Evaluate(damageData.healthAfter / damageData.maxHealth), 0.25f);
            }
        }
    }
}