using ConstStrings;
using DataClasses;
using DependencyInjection;
using DG.Tweening;
using Interfaces;
using Observables;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements.GameplayTab
{
    public class HealthBarDisplayer : MonoBehaviour, IInitializable, IDisposable, INeedDependencyInjection
    {
        [Header("Components")]
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Image _healthBarFill;

        [Header("Settings")]
        [SerializeField] private Gradient _healthBarGradient;

        [Header("DI")]
        [Inject(ObservableValue_ConstStrings.playerHealth)][SerializeField] private ObservableValue<PlayerHealthData> _playerHealthValue;

        private void Awake()
        {
            _healthBar = GetComponentInChildren<Slider>(true);
            _healthBarFill = _healthBar.fillRect.GetComponent<Image>();

            _healthBar.maxValue = 100;
        }

        private void Start()
        {
            DependencyContext.diBox.InjectDataTo(this);

            Initialize();
        }

        public void Initialize()
        {
            _playerHealthValue.AddListener(OnPlayerHealthValueChanged);

            OnPlayerHealthValueChanged(_playerHealthValue.value);
        }

        public void Dispose()
        {
            _playerHealthValue.AddListener(OnPlayerHealthValueChanged);
        }

        private void OnPlayerHealthValueChanged(PlayerHealthData damageData)
        {
            if (_healthBar != null)
            {
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