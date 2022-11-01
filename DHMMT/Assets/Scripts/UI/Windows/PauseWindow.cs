using Helpers;
using PlayerInputHolder;
using System;
using UI.Canvases;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Window
{
    public class PauseWindow : UICanvasBase
    {
        [SerializeField] private Input_SO _input;

        public override void Enable(float? duration = null)
        {
            base.Enable(duration);

            Cursor.lockState = CursorLockMode.None;

            _input.input.Gameplay.Pause.performed -= OnPause;

            _input.input.Gameplay.Disable();
            _input.input.UI.Enable();
        }

        private void OnPause(InputAction.CallbackContext obj)
        {
            Open();
        }

        public override void Disable(float? duration = null)
        {
            _input.input.Gameplay.Pause.performed += OnPause;
            base.Disable(duration);
        }
    }
}
