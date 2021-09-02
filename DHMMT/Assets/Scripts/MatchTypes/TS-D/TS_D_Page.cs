using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_D_Page : MonoBehaviour
{
    public static TS_D_Page instance;

    [SerializeField] MatchSO matchSO;

    [SerializeField] GameObject GamePlayWindow;

    [Header("Win Window")]
    [SerializeField] GameObject WinWindow;
    [SerializeField] CurrentScore currentScore;
    [SerializeField] YourRecord yourRecord;

    void Start()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);

        SecondsCount.instance.BegginCountDown(3, 20);
    }

    void OnEnable()
    {
        if (SecondsCount.instance != null)
        {
            SecondsCount.instance.BegginCountDown(0, SecondsCount.instance.GetSeconds());
        }
    }

    public void OnLoose()
    {
        PlayerKillCount.instance.KillCount -= 10;
    }

    public void OnWin()
    {
        if (PlayerKillCount.instance.GetKillCount() < matchSO.GetRecordForTheScene())
        {

        }
        else
        {
            matchSO.SetRecordForTheScene(PlayerKillCount.instance.GetKillCount());
        }

        yourRecord.SetRecotrdText(matchSO.GetRecordForTheScene());
        currentScore.SetScoreText(PlayerKillCount.instance.GetKillCount());

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
