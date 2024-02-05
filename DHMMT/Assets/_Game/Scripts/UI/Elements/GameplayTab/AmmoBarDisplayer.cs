using ConstStrings;
using DataClasses;
using DependencyInjection;
using DG.Tweening;
using Helpers;
using Interfaces;
using Observables;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements.GameplayTab
{
    public class AmmoBarDisplayer : MonoBehaviour, IInitializable, IDisposable, INeedDependencyInjection
    {
        [Header("Components")]
        [SerializeField] private Slider _ammoBar;
        [SerializeField] private Image _ammoBarFill;
        [SerializeField] private TextMeshProUGUI _ammoText;

        [Header("Settings")]
        [SerializeField] private Gradient _ammoBarGradient;

        [Inject(ObservableValue_ConstStrings.playerWeaponData)] private ObservableValue<PlayerWeaponData> _playerWeaponData;

        private void Awake()
        {
            _ammoBar = GetComponentInChildren<Slider>(true);
            _ammoBarFill = _ammoBar.fillRect.GetComponent<Image>();

            _ammoBar.maxValue = 100;
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);

            _playerWeaponData.AddListener(OnPlayerWeaponChanged);

            OnPlayerWeaponChanged(_playerWeaponData.value);
        }

        public void Dispose()
        {
            _playerWeaponData.RemoveListener(OnPlayerWeaponChanged);
        }

        private void OnPlayerWeaponChanged(PlayerWeaponData singleShootData)
        {
            if (_ammoBar != null)
            {
                _ammoBar.maxValue = singleShootData.maxAmmo;

                _ammoBar.DOKill();
                _ammoBar.DOValue(singleShootData.currentAmmo, 0.5f);
            }

            if (_ammoBarFill != null)
            {
                _ammoBarFill.DOKill();
                _ammoBarFill.DOColor(_ammoBarGradient.Evaluate(NumberHelper.GetPercentageOf1(singleShootData.currentAmmo, singleShootData.maxAmmo)), 0.25f);
            }

            if (_ammoText != null)
            {
                _ammoText.text = $"{singleShootData.currentAmmo}/{singleShootData.maxAmmo}";
            }
        }
    }
}