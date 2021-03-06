using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour, IUIPage
{
    // Gameplay canvas

    public static GameplayUI instance;

    public GameObject Page { get => _page; set => _page = value; }
    [SerializeField] private GameObject _page;

    public GameObject Standart;

    public GameObject E_F_H_page;
    public GameObject SH_TH_page;
    public GameObject TS_D_page;
    public GameObject WH_L_N_page;
    public GameObject B_F_page;

    private void Awake()
    {
        instance = this;
    }

    public void Enable(GameObject page)
    {
        page.SetActive(true);
    }

    public void Disable(GameObject page)
    {
        page.SetActive(false);
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
