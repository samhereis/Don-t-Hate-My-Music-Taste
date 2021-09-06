using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_TH_MatchController : MonoBehaviour, IMatchLoosable, IMatchWinable
{
    void OnEnable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.SH_TH_page);
    }

    public void Loose()
    {
        SH_TH_Page.instance.OnLoose();

        Destroy(gameObject);
    }

    public void Win()
    {
        SH_TH_Page.instance.OnWin();
    }

    void OnDisable()
    {
        GameplayUI.instance.Disable(GameplayUI.instance.SH_TH_page);
        StopAllCoroutines();
    }
}
