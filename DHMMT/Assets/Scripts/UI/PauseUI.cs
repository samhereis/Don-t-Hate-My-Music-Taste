using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour, IUIPage
{
    public static PauseUI instance;

    public GameObject Page { get => _page; set => _page = value; }
    [SerializeField] GameObject _page;

    void Awake()
    {
        instance = this;
    }

    public void Enable()
    {
        PauseUnpause.SetPause(true);

        GameplayUI.instance.Disable();

        Page.SetActive(true);
    }

    public void Disable()
    {
        PauseUnpause.SetPause(false);

        GameplayUI.instance.Enable();

        Page.SetActive(false);
    }
}
