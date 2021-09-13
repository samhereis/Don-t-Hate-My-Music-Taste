using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputEnabler : MonoBehaviour
{
    // update player's input when an object that this script is attached on enables/disables 

    public PlayerInput.InputState OnEnableEnable;
    public PlayerInput.InputState OnDisableEnable;

    private void OnEnable()
    { 
        PlayerInput.PlayersInputState.Enable(); PlayerInput.SetInput(OnEnableEnable); 
    }

    private void OnDisable()
    { 
        PlayerInput.SetInput(OnDisableEnable); PlayerInput.PlayersInputState.Disable(); 
    }
}
