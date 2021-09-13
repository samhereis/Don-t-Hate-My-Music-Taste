using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TS_D_Page : MonoBehaviour
{
    // Controll main UI on "TS-D" map. "TS-D_MatchType" controls main player's part on this map

    public static TS_D_Page instance;

    [SerializeField] private MatchSO _matchSO;

    [SerializeField] private GameObject _gamePlayWindow;

    [Header("Win Window")]
    [SerializeField] private GameObject _winWindow;
    [SerializeField] private CurrentScore _currentScore;
    [SerializeField] private YourRecord _yourRecord;

    void Start()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);

        SecondsCount.instance.BegginCountDown(0, 60);
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
        if (PlayerKillCount.instance.GetKillCount() < _matchSO.GetRecordForTheScene())
        {

        }
        else
        {
            _matchSO.SetRecordForTheScene(PlayerKillCount.instance.GetKillCount());
        }

        _yourRecord.SetRecotrdText(_matchSO.GetRecordForTheScene());
        _currentScore.SetScoreText(PlayerKillCount.instance.GetKillCount());

        PauseUnpause.SetPause(true);
        _winWindow.SetActive(true);
        _gamePlayWindow.SetActive(false);

        SecondsCount.instance.Stop();
    }

    public void Replay()
    {
        PauseUnpause.SetPause(false);
        StartCoroutine(SceneLoadController.LoadScene(3));
    }
}
