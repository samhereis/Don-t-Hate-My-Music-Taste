using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PlayerInputHolder
{
    [CreateAssetMenu(fileName = "Input", menuName = "Scriptables/Input")]
    public class Input_SO : ScriptableObject
    {
        [field: SerializeField] public InputActions input { get; private set; }
        public readonly UnityEvent<bool> onInputStatusChanged = new UnityEvent<bool>();

        private void OnEnable()
        {
            input = new InputActions();
            Enable();
        }

        public void Enable()
        {
            input?.Enable();
            onInputStatusChanged?.Invoke(true);
        }

        public void Disable()
        {
            input?.Disable();
            onInputStatusChanged?.Invoke(false);
        }
    }
}