using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseUnpause : MonoBehaviour
{
    void OnEnable ()
    {
        PlayerInput.input.Gameplay.Pause.performed += Pause;
    }

    void OnDisable()
    {
        PlayerInput.input.Gameplay.Pause.performed -= Pause;
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        PauseUI.instance.Enable();
    }

    public static void SetPause(bool pause)
    {
        if(pause == true)
        {
            AudioListener.pause = true;

            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;

            PlayerInput.SetInput(PlayerInput.InputState.UI);
        }
        else
        {
            AudioListener.pause = false;

            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;

            PlayerInput.SetInput(PlayerInput.InputState.Gameplay);
        }
    }
}
