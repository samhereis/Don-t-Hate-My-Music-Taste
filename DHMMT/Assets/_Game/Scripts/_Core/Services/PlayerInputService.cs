using UnityEngine.InputSystem;

namespace Services
{
    public class PlayerInputService : InputsService
    {
        public InputActionsHolder input { get; set; }

        public PlayerInputService()
        {
            input = new InputActionsHolder();
            Enable();
        }

        public void Enable()
        {
            input.Enable();
        }

        public void Disable()
        {
            input.Disable();
        }
        
        public void EnableOnUIBackPressed()
        {
            DisableOnUIBackPressed();
            input.UI.Back.started += OnUIBackPressed;
        }

        public void DisableOnUIBackPressed()
        {
            input.UI.Back.started -= OnUIBackPressed;
        }

        private void OnUIBackPressed(InputAction.CallbackContext context)
        {
            onUIBackPressed?.Invoke();
        }
    }
}