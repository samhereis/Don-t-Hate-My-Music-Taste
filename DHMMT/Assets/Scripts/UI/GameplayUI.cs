using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour, IUIPage
{
    public static GameplayUI instance;

    public GameObject Page { get => _page; set => _page = value; }
    [SerializeField] GameObject _page;

    void Awake()
    {
        instance = this;
    }

    public void Enable()
    {
        Page.SetActive(true); 
    }

    public void Disable()
    {
        Page.SetActive(false);
    }
}
