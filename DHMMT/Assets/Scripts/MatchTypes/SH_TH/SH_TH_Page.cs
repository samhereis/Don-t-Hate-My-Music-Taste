using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_TH_Page : MonoBehaviour
{
    // Controll main UI on "SH-TH" map. "SH-TH_MatchType" controls main player's part on this map

    public static SH_TH_Page instance;

    [SerializeField] private MatchSO _matchSO;

    [SerializeField] private GameObject _gamePlayWindow;
    [SerializeField] private GameObject _winWindow;

    private void Start()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);

        PlayerKillCount.instance.StartCheckingForEnemies(3);
    }

    public void OnLoose()
    {
        Spawner.instance.SpawnEnemies(15);

        Spawner.instance.RepawnPlayer(SpawnPoints.instance.GetRandomSpawnForPlayer());
    }

    public void OnWin()
    {
        PauseUnpause.SetPause(true);
        _winWindow.SetActive(true);
        _gamePlayWindow.SetActive(false);
    }

    public void Replay()
    {
        PauseUnpause.SetPause(false);
        StartCoroutine(SceneLoadController.LoadScene(4));
    }
}
