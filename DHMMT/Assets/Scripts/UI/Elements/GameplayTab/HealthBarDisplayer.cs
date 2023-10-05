using ConstStrings;
using DataClasses;
using DG.Tweening;
using DI;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Values;

namespace UI.Elements.GameplayTab
{
    public class HealthBarDisplayer : MonoBehaviour, IInitializable, IClearable, IDIDependent
    {
        [Header("Components")]
        [SerializeField] private Slider _healthBar;
        [SerializeField] private Image _healthBarFill;

        [Header("Settings")]
        [SerializeField] private Gradient _healthBarGradient;

        [Header("DI")]
        [DI(Event_DIStrings.playerHealth)][SerializeField] private ValueEvent<PlayerHealthData> _playerHealthValue;

        private void Awake()
        {
            _healthBar = GetComponentInChildren<Slider>(true);
            _healthBarFill = _healthBar.fillRect.GetComponent<Image>();

            _healthBar.maxValue = 100;
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();
            Initialize();
        }

        public void Initialize()
        {
            _playerHealthValue.AddListener(OnPlayerHealthValueChanged);

            OnPlayerHealthValueChanged(_playerHealthValue.value);
        }

        public void Clear()
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