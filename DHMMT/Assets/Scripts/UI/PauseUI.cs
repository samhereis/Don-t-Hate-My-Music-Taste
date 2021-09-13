using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseUI : MonoBehaviour, IUIPage
{
    // Pause menu manage

    public static PauseUI instance;

    public GameObject Page { get => _page; set => _page = value; }
    [SerializeField] GameObject _page;

    public void Enable()
    {
        PauseUnpause.SetPause(true);

        GameplayUI.instance.Disable();

        Page.SetActive(true);
    }

    public void Disable()
    {
        PauseUnpause.SetPause(false);

        Page.SetActive(false);

        GameplayUI.instance.Enable();
    }

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = this;
    }
}
