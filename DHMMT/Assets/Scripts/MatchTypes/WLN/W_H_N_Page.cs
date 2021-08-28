using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_H_N_Page : MonoBehaviour
{
    public static W_H_N_Page instance;

    [SerializeField] GameObject GamePlayWindow;
    [SerializeField] GameObject WinWindow;

    void Start()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        SecondsCount.instance.Stop();
        SecondsCount.instance.BegginCountDown(3, 10);
    }

    public void OnLoose()
    {
        PlayerKillCount.instance.KillCount -= 10;
    }

    public void OnWin()
    {
        PauseUnpause.SetPause(true);
        WinWindow.SetActive(true);
        GamePlayWindow.SetActive(false);

        SecondsCount.instance.Stop();
    }

    public void Replay()
    {
        PauseUnpause.SetPause(false);
        StartCoroutine(MenuStatics.LoadScene(5));
    }
}
