using Scriptables;
using UnityEngine;

public class inputEnabler : MonoBehaviour
{
    private enum InputStates { Gameplay, UI }
    [SerializeField] private InputStates _onEnableSetInputState;

    [SerializeField] private Input_SO _inputContainer;

    private void OnEnable()
    {
        _inputContainer.Enable();

        if (_onEnableSetInputState == InputStates.Gameplay)
        {
            _inputContainer.input.Gameplay.Enable();
        }
        else
        {
            _inputContainer.input.UI.Enable();
        }
    }
}
