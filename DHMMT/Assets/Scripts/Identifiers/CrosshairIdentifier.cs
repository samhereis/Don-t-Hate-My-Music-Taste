using Configs;
using ConstStrings;
using DG.Tweening;
using DI;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Identifier
{
    public class CrosshairIdentifier : MonoBehaviour, IInitializable, IDIDependent
    {
        [SerializeField] private Image _crosshairImage;

        [Header("DI")]
        [DI(DIStrings.uiConfigs)][SerializeField] private UIConfigs _uiConfigs;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            (this as IDIDependent).LoadDependencies();

            if (_uiConfigs != null)
            {
                _crosshairImage.DOColor(_uiConfigs.gameplayMenuConfigs.crosshairColor, 1);
            }
        }

        public void Show()
        {
            transform.DOScale(1, 0.25f);
        }

        public void Hide()
        {
            transform.DOScale(0, 0.25f);
        }
    }
}