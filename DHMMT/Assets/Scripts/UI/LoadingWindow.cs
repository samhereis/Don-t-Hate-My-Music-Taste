using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingWindow : MonoBehaviour
{
    // Loading window data

    public static LoadingWindow instance;

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}
