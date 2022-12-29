using Helpers;
using PlayerInputHolder;
using System;
using UI.Canvases;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Window
{
    public class GameplayWindow : CanvasBase
    {
        [SerializeField] private Input_SO _input;
        public override void Enable(float? duration = null)
        {
            base.Enable(duration);
            Cursor.lockState = CursorLockMode.Locked;

            _input.input.UI.Disable();
            _input.input.Gameplay.Enable();
        }

        public override void Disable(float? duration = null)
        {
            base.Disable(duration);
        }
    }
}