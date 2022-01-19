using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Input", menuName = "Scriptables/Input")]
    public class Input_SO : ScriptableObject
    {
        [SerializeField] private InputSettings _input;
        public InputSettings input => _input;

        public readonly UnityEvent<bool> onInputStatusChanged = new UnityEvent<bool>();

        private void OnEnable()
        {
            _input = new InputSettings();

            Enable();
        }

        public void Enable()
        {
            _input?.Enable();

            onInputStatusChanged?.Invoke(true);
        }

        public void Disable()
        {
            _input?.Disable();

            onInputStatusChanged?.Invoke(false);
        }
    }
}