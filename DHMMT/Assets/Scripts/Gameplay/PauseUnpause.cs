using PlayerInputHolder;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PauseUnpause : MonoBehaviour //TODO: complete this
    {
        [SerializeField] private Input_SO _inputContainer;
        private InputActions _input => _inputContainer.input;

        private void OnEnable()
        {
            _input.Gameplay.Pause.performed += Pause;
        }

        private void OnDisable()
        {
            _input.Gameplay.Pause.performed -= Pause;
        }

        private void Pause(InputAction.CallbackContext obj)
        {

        }
    }
}