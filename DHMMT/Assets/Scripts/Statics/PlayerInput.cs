using System;
using UnityEngine;
using UnityEngine.Events;

public static class PlayerInput
{
                #region InputData
    public enum  InputState { Gameplay, UI }
    public static InputState state;
    public static InputSettings input = new InputSettings();
                #endregion InputData

                #region InputsEvents
    public static Action OnFire;
    public static Action OnReload;
    public static Action<Transform> OnAim;
                #endregion

                #region  Methods
    public static void SetInput(InputState setTo)
    {
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
                #endregion  Methods
}
