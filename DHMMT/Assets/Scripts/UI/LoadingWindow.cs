using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingWindow : MonoBehaviour
{
    // Loading window data

    public static LoadingWindow instance;

    public GameObject Window;

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }
}
