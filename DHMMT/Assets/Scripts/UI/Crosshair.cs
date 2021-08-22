using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public static Crosshair instance;

    void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
