using Scriptables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseUnpause : MonoBehaviour
{
    [SerializeField] private Input_SO _inputContainer;
    private InputSettings _input => _inputContainer.input;

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
