using DI;
using UI.Popups;
using UnityEngine;

namespace UI.Windows.GameplayMenus
{
    public class EFH_GameplayMenu : MonoBehaviour, IDIDependent
    {
        [Header("Components")]
        [SerializeField] private GameplayMenu _gameplayMenu;
        [field: SerializeField] public StayUnderTheLight_Popup stayUnderTheLight_Popup;

        private void Awake()
        {
            _gameplayMenu.onOpen += OnGameplayMenuOpen;
            _gameplayMenu.onClose += OnGameplayMenuClose;

            stayUnderTheLight_Popup = GetComponentInChildren<StayUnderTheLight_Popup>(true);
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();
        }

        private void OnDestroy()
        {
            _gameplayMenu.onOpen -= OnGameplayMenuOpen;
            _gameplayMenu.onClose -= OnGameplayMenuClose;
        }

        private void OnGameplayMenuOpen()
        {

        }

        private void OnGameplayMenuClose()
        {

        }
    }
}