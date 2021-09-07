using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class E_F_H_Page : MonoBehaviour
{
    public static E_F_H_Page instance;

    [SerializeField] MatchSO matchSO;

    [SerializeField] GameObject GamePlayWindow;

    [Header("Win Window")]
    [SerializeField] GameObject WinWindow;
    [SerializeField] CurrentScore currentScore;
    [SerializeField] YourRecord yourRecord;

    [Header("Loose Window")]
    [SerializeField] GameObject LooseWindow;

    void Awake()
    {
        MessageScript.instance.ShowMessage(MessageScript.instance.StayUnderTheLightAndFindTheExit, 10);
    }

    void OnEnable()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        GamePlayWindow.SetActive(true);

        if(SecondsCount.instance != null)
        {
            SecondsCount.instance.Beggin(0, SecondsCount.instance.GetSeconds());
        }
    }

    public void OnWin()
    {
        if(SecondsCount.instance.GetSeconds() < matchSO.GetRecordForTheScene())
        {
            matchSO.SetRecordForTheScene(SecondsCount.instance.GetSeconds());
        }
        else if (matchSO.GetRecordForTheScene() < 5)
        {
            matchSO.SetRecordForTheScene(SecondsCount.instance.GetSeconds());
        }

        yourRecord.SetRecotrdText(matchSO.GetRecordForTheScene());
        currentScore.SetScoreText(SecondsCount.instance.GetSeconds());

        PauseUnpause.SetPause(true);
        GamePlayWindow.SetActive(false);
        WinWindow.SetActive(true);
    }

    public void OnLoose()
    {
        currentScore.SetScoreText(SecondsCount.instance.GetSeconds());
        yourRecord.SetRecotrdText(PlayerPrefs.GetInt(""));

        PauseUnpause.SetPause(true);
        GamePlayWindow.SetActive(false);
        LooseWindow.SetActive(true);
    }

    public void OnReplay()
    {
        Destroy(PlayerHealthData.instance.gameObject);
        PauseUnpause.SetPause(false);
        StartCoroutine(MenuStatics.LoadScene(2));
    }
}
