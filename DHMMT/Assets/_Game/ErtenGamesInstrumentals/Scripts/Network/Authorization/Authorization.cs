using Helpers;
using UI;
using UI.Canvases;
using UnityEngine;

namespace Authorization.UI
{
    public sealed class Authorization : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private MenuBase _openIfNotLoggedIn;
        [SerializeField] private MenuBase _mainMenuCanvas;

        [SerializeField] private SignIn _signIn;
        [SerializeField] private SignUp _signUp;
        [SerializeField] private MenuBase _codeVerificator;

        private void Awake()
        {
            _openIfNotLoggedIn?.Disable(0);
            _mainMenuCanvas?.Disable(0);
            _signIn?.Disable(0);
            _signUp?.Disable(0);
            _codeVerificator?.Disable(0);
        }

        private void OnEnable()
        {
            TryGetIntoGame();
        }

        public void TryGetIntoGame()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                MessageToUserMenu.instance.Log("You are offline");
            }
        }

        private void GoToMainMenu()
        {
#if DoTweenInstalled
            _canvasGroup.FadeDown(0.25f);
            _mainMenuCanvas?.Enable();
#endif
        }
    }
}