using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_TH_Page : MonoBehaviour
{
    public static SH_TH_Page instance;

    [SerializeField] MatchSO matchSO;

    [SerializeField] GameObject GamePlayWindow;
    [SerializeField] GameObject WinWindow;

    void Start()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
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
    }

    public void Replay()
    {
        PauseUnpause.SetPause(false);
        StartCoroutine(MenuStatics.LoadScene(5));
    }
}
