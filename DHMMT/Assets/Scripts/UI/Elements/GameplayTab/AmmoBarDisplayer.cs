using ConstStrings;
using DataClasses;
using DG.Tweening;
using DI;
using Helpers;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Values;

namespace UI.Elements.GameplayTab
{
    public class AmmoBarDisplayer : MonoBehaviour, IInitializable, IClearable, IDIDependent
    {
        [Header("Components")]
        [SerializeField] private Slider _ammoBar;
        [SerializeField] private Image _ammoBarFill;
        [SerializeField] private TextMeshProUGUI _ammoText;

        [Header("Settings")]
        [SerializeField] private Gradient _ammoBarGradient;

        [Header("DI")]
        [DI(Event_DIStrings.playerWeaponData)][SerializeField] private ValueEvent<PlayerWeaponData> _playerWeaponData;

        private void Awake()
        {
            _ammoBar = GetComponentInChildren<Slider>(true);
            _ammoBarFill = _ammoBar.fillRect.GetComponent<Image>();

            _ammoBar.maxValue = 100;
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();
            Initialize();
        }

        public void Initialize()
        {
            _playerWeaponData.AddListener(OnPlayerWeaponChanged);
        }

        public void Clear()
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