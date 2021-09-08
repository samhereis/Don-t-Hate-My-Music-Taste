using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_F_H_MatchController : MonoBehaviour, IMatchWinable, IMatchLoosable
{
    void OnEnable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.E_F_H_page);

        SecondsCount.instance.Beggin(0, 0);

        StartCoroutine(PlayerHealthData.instance.TakeDamageContinuously(2, 20));

        GetComponent<PlayerJump>().doubleJumpable = false;
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
