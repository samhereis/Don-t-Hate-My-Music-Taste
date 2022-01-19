using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_H_N_Page : MonoBehaviour
{
    // Controll main UI on "WH-L-N" map. "W_H_N_MatchType" controls main player's part on this map

    public static W_H_N_Page instance;

    [SerializeField] private  MatchSO matchSO;

    [SerializeField] private GameObject _gamePlayWindow;

    [Header("Win Window")]
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private CurrentScore _currentScore;
    [SerializeField] private YourRecord _yourRecord;

    [Header("Helpers")]
    [SerializeField] private SceneLoader _sceneLoader;

    private void Start()
    {
        instance ??= this;

        SecondsUI.instance?.Stop();
        SecondsUI.instance?.BegginCountDown(0, 500);
    }

    private void OnEnable()
    {
        if (SecondsUI.instance != null)
        {
            SecondsUI.instance.BegginCountDown(0, SecondsUI.instance.GetSeconds());
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

        _yourRecord.SetRecotrdText(matchSO.GetRecordForTheScene());
        _currentScore.SetScoreText(PlayerKillCount.instance.GetKillCount());

        PauseUnpause.SetPause(true);
        _winWindow.SetActive(true);
        _gamePlayWindow.SetActive(false);

        SecondsUI.instance.Stop();
    }

    public void Replay()
    {
        PauseUnpause.SetPause(false);
        _sceneLoader.LoadScene(6);
    }
}
