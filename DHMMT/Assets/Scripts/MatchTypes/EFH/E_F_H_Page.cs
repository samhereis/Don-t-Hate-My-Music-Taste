using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class E_F_H_Page : MonoBehaviour
{
    public static E_F_H_Page instance;

    public GameObject GamePlayWindow { get => _gamePlayWindow; }
    [SerializeField] GameObject _gamePlayWindow;

    public GameObject WinWindow { get => _winWindow; }
    [SerializeField] GameObject _winWindow;

    public GameObject LooseWindow { get => _loseWindow; }
    [SerializeField] GameObject _loseWindow;

    private void OnEnable()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        GamePlayWindow.SetActive(true);
    }

    public void OnWin()
    {
        PauseUnpause.SetPause(true);
        GamePlayWindow.SetActive(false);
        WinWindow.SetActive(true);
    }

    public void OnLoose()
    {
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
