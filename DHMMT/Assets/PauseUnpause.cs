using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUnpause : MonoBehaviour
{
    void OnEnable ()
    {
        PlayerInput.input.Gameplay.Pause.performed += _ => PauseUI.instance.Enable();
    }

    public static void SetPause(bool pause)
    {
        if(pause == true)
        {
            Cals.instance.PauseMusic(true);
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;

            PlayerInput.SetInput(PlayerInput.InputState.UI);
        }
        else
        {
            Cals.instance.PauseMusic(false);
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;

            PlayerInput.SetInput(PlayerInput.InputState.Gameplay);
        }
    }
}
