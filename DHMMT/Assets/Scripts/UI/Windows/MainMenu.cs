using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class MainMenu : CanvasWindowBase
    {
        [Header("Menus")]
        [SerializeField] private SceneSelectionMenu _sceneSelectionMenu;
        [SerializeField] private SettingsMenu _settingsMenu;

        [Header("Buttons")]
        [SerializeField] private Button _sceneSelectionMenuButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        public override void Enable(float? duration = null)
        {
            base.Enable(duration);

            SubscribeToEvents();
        }

        public override void Disable(float? duration = null)
        {
            UnsubscribeFromEvents();

            base.Disable(duration);
        }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();

            _sceneSelectionMenuButton?.onClick.AddListener(OnClickedSceneSelectionMenuButton);
            _settingsButton?.onClick.AddListener(OnClickedSettingsButton);
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();

            _sceneSelectionMenuButton?.onClick.RemoveListener(OnClickedSceneSelectionMenuButton);
            _settingsButton?.onClick.RemoveListener(OnClickedSettingsButton);
        }

        public void OnClickedSceneSelectionMenuButton()
        {
            _sceneSelectionMenu.Enable();
        }

        public void OnClickedSettingsButton()
        {
            _settingsMenu.Enable();
        }

        public void OnClickedQuitButton()
        {

        }
    }
}