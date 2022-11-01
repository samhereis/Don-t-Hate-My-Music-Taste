using Helpers;
using PlayerInputHolder;
using UI.Canvases;
using UnityEngine;

namespace UI.Window
{
    public class GameplayWindow : UICanvasBase
    {
        [SerializeField] private Input_SO _input;

        public override void Enable(float? duration = null)
        {
            base.Enable(duration);
            Cursor.lockState = CursorLockMode.Locked;

            _input.input.UI.Disable();
            _input.input.Gameplay.Enable();
        }
    }
}