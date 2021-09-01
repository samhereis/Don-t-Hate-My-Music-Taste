using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_F_H_MatchController : MonoBehaviour, IMatchWinable, IMatchLosable
{
    void OnEnable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.E_F_H_page);
        SecondsCount.instance.Stop();
        SecondsCount.instance.Beggin(1);
        StartCoroutine(PlayerHealthData.instance.TakeDamageContinuously(2, 20));
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
        StopAllCoroutines();
    }
}
