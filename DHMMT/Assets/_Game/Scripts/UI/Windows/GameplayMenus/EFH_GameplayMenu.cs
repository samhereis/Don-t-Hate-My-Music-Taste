using UI.Canvases;
using UI.Popups;
using UnityEngine;

namespace UI.Windows.GameplayMenus
{
    public class EFH_GameplayMenu : MenuExtenderBase<GameplayMenu>
    {
        public StayUnderTheLight_Popup stayUnderTheLight_Popup
        {
            get
            {
                if (_stayUnderTheLight_Popup == null) { _stayUnderTheLight_Popup = GetComponentInChildren<StayUnderTheLight_Popup>(true); }

                return _stayUnderTheLight_Popup;
            }
        }

        [SerializeField] private StayUnderTheLight_Popup _stayUnderTheLight_Popup;

        private void Awake()
        {
            window.onEnable += OnGameplayMenuOpen;
            window.onDisable += OnGameplayMenuClose;
        }

        private void OnDestroy()
        {
            window.onEnable -= OnGameplayMenuOpen;
            window.onDisable -= OnGameplayMenuClose;
        }

        private void OnGameplayMenuOpen()
        {

        }

        private void OnGameplayMenuClose()
        {

        }
    }
}