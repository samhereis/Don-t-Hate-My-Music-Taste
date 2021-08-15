using System;
using UnityEngine;
using UnityEngine.Events;

public static class PlayerInput
{
    public enum  InputState { Gameplay, UI }
    public static InputState state;
    public static InputSettings input = new InputSettings();

    public static void SetInput(InputState setTo)
    {
        input.Enable();

        if (setTo == state) return;

        if(setTo == InputState.Gameplay)
        {
            input.Gameplay.Enable();
            input.UI.Disable();
        }
        else
        {
            input.Gameplay.Disable();
            input.UI.Enable();
        }

        state = setTo;
    }
}
