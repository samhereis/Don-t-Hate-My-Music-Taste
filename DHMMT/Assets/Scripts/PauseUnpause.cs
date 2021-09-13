using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseUnpause : MonoBehaviour
{
    public static void SetPause(bool pause)
    {
        if(pause == true)
        {
            PlayerInput.SetInput(PlayerInput.InputState.UI);

            AudioListener.pause = true;

            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            PlayerInput.SetInput(PlayerInput.InputState.Gameplay);

            AudioListener.pause = false;

            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnEnable()
    {
        PlayerInput.PlayersInputState.Gameplay.Pause.performed += Pause;
    }

    private void OnDisable()
    {
        PlayerInput.PlayersInputState.Gameplay.Pause.performed -= Pause;
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        PauseUI.instance.Enable();
    }
}
