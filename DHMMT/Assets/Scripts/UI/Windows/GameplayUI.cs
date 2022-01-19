using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    // Gameplay canvas

    public static GameplayUI instance;

    public GameObject Page { get => _page; set => _page = value; }
    [SerializeField] private GameObject _page;

    public GameObject Standart;

    private void Awake()
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
