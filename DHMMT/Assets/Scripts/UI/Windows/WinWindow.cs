using Samhereis.DI;
using Samhereis.Events;
using Samhereis.Helpers;
using Samhereis.PlayerInputHolder;
using Samhereis.UI;
using UnityEngine;

namespace UI.Window
{

    public sealed class WinWindow : UIWIndowBase
    {
        [Header("SO")]
        [SerializeField] private EventWithNoParameters _eventWithNoParameters;
        [SerializeField] private Input_SO _input;

        protected override async void Awake()
        {
            if (_input == null) _input = await AddressablesHelper.GetAssetAsync<Input_SO>(_input.GetType().ToString());
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