using Scriptables;
using Sripts;
using System.Collections;
using System.Collections.Generic;
using UI.Window;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Window
{
    public class GameplayWindow : UIWIndowBase
    {
        [SerializeField] private Input_SO _input;

        private void Awake()
        {
            if (!_input) AddressableGetter.GetAddressable<Input_SO>(nameof(Input_SO), (result) => { _input = result; });

            onAWindowOpen += OnAWindowOpen;
        }

        public override void OnAWindowOpen(UIWIndowBase uIWIndow)
        {
            if (uIWIndow is GameplayWindow == false && _isOpen)
            {
                _windowBehavior.Close();

                _isOpen = false;
            }
        }

        protected void Enable(InputAction.CallbackContext context) => Enable(); 

        public override void Enable()
        {
            _windowBehavior?.Open();
            onAWindowOpen?.Invoke(this);

            _input.input.UI.Disable();
            _input.input.Gameplay.Enable();

            _isOpen = true;
        }

        public override void Setup()
        {

        }
    }
}