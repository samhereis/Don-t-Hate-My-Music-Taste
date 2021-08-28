using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_D_Page : MonoBehaviour
{
    public static TS_D_Page instance;

    [SerializeField] GameObject GamePlayWindow;
    [SerializeField] GameObject WinWindow;

    void Start()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        SecondsCount.instance.Stop();
        SecondsCount.instance.BegginCountDown(3, 20);
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
        StartCoroutine(MenuStatics.LoadScene(3));
    }
}
