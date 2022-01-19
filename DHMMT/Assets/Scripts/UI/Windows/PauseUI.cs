using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseUI : MonoBehaviour
{
    // Pause menu manage

    public GameObject Page { get => _page; set => _page = value; }
    [SerializeField] GameObject _page;

    public void Enable()
    {
        Page.SetActive(true);
    }

    public void Disable()
    {
        Page.SetActive(false);
    }
}
