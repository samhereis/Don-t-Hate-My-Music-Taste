using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUICamera : MonoBehaviour
{
    private void OnEnable()
    {
        if(UICamera.instance != null)
        {
            UICamera.instance.SetEnabled(true);
        }
    }

    private void OnDisable()
    {
        if (UICamera.instance != null)
        {
            UICamera.instance.SetEnabled(false);
        }
    }
}
