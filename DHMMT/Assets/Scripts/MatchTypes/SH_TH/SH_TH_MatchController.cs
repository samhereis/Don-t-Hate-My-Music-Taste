using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_TH_MatchController : MonoBehaviour
{
    void OnEnable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.SH_TH_page);
    }

    public void Loose()
    {
        SecondsCount.instance.Stop();
        E_F_H_Page.instance.OnLoose();
    }

    public void Win()
    {
        E_F_H_Page.instance.OnWin();
    }

    void OnDisable()
    {
        GameplayUI.instance.Disable(GameplayUI.instance.SH_TH_page);
        StopAllCoroutines();
    }
}
