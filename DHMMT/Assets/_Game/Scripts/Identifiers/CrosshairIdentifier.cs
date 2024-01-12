using Configs;
using DependencyInjection;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Identifiers
{
    public class CrosshairIdentifier : IdentifierBase, IInitializable, INeedDependencyInjection
    {
        [SerializeField] private Image _crosshairImage;

        [Inject] private UIConfigs _uiConfigs;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            DependencyContext.diBox.InjectDataTo(this);

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