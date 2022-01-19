using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_F_Page : MonoBehaviour
{
    // Controll main UI on "SH-TH" map. "SH-TH_MatchType" controls main player's part on this map

    public static B_F_Page instance;

    [SerializeField] private MatchSO _matchSO;

    [SerializeField] private GameObject _gamePlayWindow;
    [SerializeField] private GameObject _winWindow;
    
    [Header("Helpers")]
    [SerializeField] private SceneLoader _sceneLoader;

    private void Start()
    {
        instance ??= this;
    }

    public void OnLoose()
    {

    }

    public void OnWin()
    {
        _gamePlayWindow.SetActive(false);
        _winWindow.SetActive(true);
        PauseUnpause.SetPause(true);
    }

    public void Replay()
    {
        PauseUnpause.SetPause(false);
        _sceneLoader.LoadScene(6);
    }
}
