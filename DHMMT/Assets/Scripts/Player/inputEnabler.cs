using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputEnabler : MonoBehaviour
{
    public PlayerInput.InputState OnEnableEnable;
    public PlayerInput.InputState OnDisableEnable;
    void OnEnable() { PlayerInput.input.Enable(); PlayerInput.SetInput(OnEnableEnable); }
    void OnDisable() { PlayerInput.SetInput(OnDisableEnable); PlayerInput.input.Disable(); }
}
