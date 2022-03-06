using Scriptables;
using Sripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Window
{
    public class PauseWindow : UIWIndowBase
    {
        [SerializeField] private Input_SO _input;

        private void Awake()
        {
            if (!_input) AddressableGetter.GetAddressable<Input_SO>(nameof(Input_SO), (result) => { _input = result; });

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

            if(uIWIndow is PauseWindow)
            {
                _input.input.Gameplay.Pause.performed -= Enable;
            }
        }
        protected void Enable(InputAction.CallbackContext context)
        {
            Enable();
        }

        public override void Enable()
        {
            _windowBehavior?.Open();
            onAWindowOpen?.Invoke(this);

            _input.input.Gameplay.Disable();
            _input.input.UI.Enable();

            _isOpen = true;
        }

        public override void Setup()
        {

        }
    }
}
