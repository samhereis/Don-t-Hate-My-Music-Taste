using Events;
using Helpers;
using Scriptables;
using UnityEngine;

namespace UI.Window
{

    public sealed class WinWindow : UIWIndowBase
    {
        [Header("SO")]
        [SerializeField] private EventWithNoParameters _eventWithNoParameters;
        [SerializeField] private Input_SO _input;

        private void OnValidate()
        {
            if (!_input) AddressablesHelper.LoadAndDo<Input_SO>(nameof(Input_SO), (result) => { _input = result; });
        }

        protected override void Awake()
        {
            if (!_input) AddressablesHelper.LoadAndDo<Input_SO>(nameof(Input_SO), (result) => { _input = result; });
            _eventWithNoParameters.AdListener(Enable);
        }

        public override void OnAWindowOpen(UIWIndowBase uIWIndow)
        {
            if (uIWIndow is not WinWindow)
            {
                _windowBehavior.Close();

                _isOpen = false;
            }
        }

        public override void Enable()
        {
            _eventWithNoParameters.RemoveListener(Enable);
            _windowBehavior.Open();
            onAWindowOpen?.Invoke(this);

            _input.input.Gameplay.Disable();
            _input.input.UI.Enable();
            Cursor.lockState = CursorLockMode.None;
        }

        public override void Setup()
        {

        }
    }
}