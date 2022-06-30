using Samhereis.DI;
using Samhereis.Helpers;
using Samhereis.PlayerInputHolder;
using Samhereis.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Window
{
    public class PauseWindow : UIWIndowBase
    {
        [SerializeField] private Input_SO _input;

        protected override async void Awake()
        {
            if (_input == null) _input = await AddressablesHelper.GetAssetAsync<Input_SO>(_input.GetType().ToString());

            onAWindowOpen += OnAWindowOpen;
        }

        public override void OnAWindowOpen(UIWIndowBase uIWIndow)
        {
            if (uIWIndow is GameplayWindow)
            {
                _input.input.Gameplay.Pause.performed += Enable;
                _windowBehavior?.Close();
                _isOpen = false;
            }
        }

        protected void Enable(InputAction.CallbackContext context)
        {
            Enable();
        }

        public override void Enable()
        {
            Cursor.lockState = CursorLockMode.None;

            _windowBehavior?.Open();
            onAWindowOpen?.Invoke(this);

            _input.input.Gameplay.Pause.performed -= Enable;

            _input.input.Gameplay.Disable();
            _input.input.UI.Enable();

            _isOpen = true;
        }

        public override void Setup()
        {

        }
    }
}
