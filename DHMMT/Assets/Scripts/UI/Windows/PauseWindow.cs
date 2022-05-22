using Scriptables;
using Helpers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Window
{
    public class PauseWindow : UIWIndowBase
    {
        [SerializeField] private Input_SO _input;

        protected override void Awake()
        {
            if (_input != null) AddressablesHelper.LoadAndDo<Input_SO>(nameof(Input_SO), (result) => { _input = result; });

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
