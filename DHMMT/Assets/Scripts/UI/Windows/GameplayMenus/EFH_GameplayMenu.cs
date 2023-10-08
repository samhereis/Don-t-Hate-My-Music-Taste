using DI;
using UI.Popups;
using UnityEngine;

namespace UI.Windows.GameplayMenus
{
    public class EFH_GameplayMenu : MonoBehaviour, IDIDependent
    {
        [Header("Components")]
        [field: SerializeField] public GameplayMenu gameplayMenu;
        [field: SerializeField] public StayUnderTheLight_Popup stayUnderTheLight_Popup;

        private void Awake()
        {
            if (gameplayMenu == null) { gameplayMenu = GetComponent<GameplayMenu>(); }

            gameplayMenu.onOpen += OnGameplayMenuOpen;
            gameplayMenu.onClose += OnGameplayMenuClose;

            stayUnderTheLight_Popup = GetComponentInChildren<StayUnderTheLight_Popup>(true);
        }

        private void Start()
        {
            (this as IDIDependent).LoadDependencies();
        }

        private void OnDestroy()
        {
            gameplayMenu.onOpen -= OnGameplayMenuOpen;
            gameplayMenu.onClose -= OnGameplayMenuClose;
        }

        private void OnGameplayMenuOpen()
        {

        }

        private void OnGameplayMenuClose()
        {

        }
    }
}