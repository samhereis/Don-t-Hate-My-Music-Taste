using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public static Crosshair instance;

    private void Awake()
    {
        instance ??= this;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
