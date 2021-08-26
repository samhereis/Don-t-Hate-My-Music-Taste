using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFHMatchController : MonoBehaviour, IMatchController
{
    void OnEnable()
    {
        GameplayUI.instance.Enable(GameplayUI.instance.E_F_H_page);
        SecondsCount.instance.Stop();
        SecondsCount.instance.Beggin(1);
    }

    public void Loose()
    {
        SecondsCount.instance.Stop();
        E_F_H_Page.instance.Loose();
    }
}
