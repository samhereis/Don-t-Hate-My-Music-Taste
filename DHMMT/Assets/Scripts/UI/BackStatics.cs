using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public static class BackStatics
{
    public static IUIPage page;

    public static GameObject toEnable, ToDisable;

    public static bool IsUIPage;

    public static Button button;

    public static void GetBack()
    {
        if (IsUIPage == true && page != null)
        {
            page.Disable();
        }
        else if (ToDisable != null)
        {
            ToDisable.SetActive(false);
            toEnable.SetActive(true);
        }
    }

    public static void Back(InputAction.CallbackContext context)
    {
        button.onClick.Invoke();
    }
}
