using Events;
using Helpers;
using PlayerInputHolder;
using UI.Canvases;
using UnityEngine;

namespace UI.Window
{
    public sealed class WinWindow : UICanvasBase
    {
        [Header("SO")]
        [SerializeField] private EventWithNoParameters _onWin;
        [SerializeField] private Input_SO _input;

        public override void Enable(float? duration = null)
        {
            base.Enable(duration);

            _onWin.RemoveListener(Open);

            _input.input.Gameplay.Disable();
            _input.input.UI.Enable();
            Cursor.lockState = CursorLockMode.None;
        }
    }
}