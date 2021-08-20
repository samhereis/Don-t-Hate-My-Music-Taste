using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI instance;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        PlayerInput.input.UI.Back.performed += BackStatics.Back;
    }

    void OnDisable()
    {
        PlayerInput.input.UI.Back.performed -= BackStatics.Back;
    }
}
