using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public static class PlayerInput
{
    // Player's input data manager

    public enum  InputState { Gameplay, UI }
    public static InputState State;
    public static InputSettings PlayersInputState = new InputSettings();

    public static void SetInput(InputState setTo)
    {
        PlayersInputState.Enable();

        if (setTo == State) return;

        if(setTo == InputState.Gameplay)
        {
            PlayersInputState.Gameplay.Enable();
            PlayersInputState.UI.Disable();
        }
        else
        {
            PlayersInputState.Gameplay.Disable();
            PlayersInputState.UI.Enable();
        }

        State = setTo;
    }
}
