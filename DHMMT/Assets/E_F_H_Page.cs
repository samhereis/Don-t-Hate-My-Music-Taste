using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class E_F_H_Page : MonoBehaviour, IPage
{
    public static E_F_H_Page instance;

    [SerializeField] GameObject Camera;

    public GameObject GamePlayWindow;
    public GameObject WinWindow;
    public GameObject LoseWindow;

    private void OnEnable()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        GamePlayWindow.SetActive(true);
    }

    public void Win()
    {
        PauseUnpause.SetPause(true);
        GamePlayWindow.SetActive(false);
        WinWindow.SetActive(true);
    }

    public void Loose()
    {
        PauseUnpause.SetPause(true);
        GamePlayWindow.SetActive(false);
        LoseWindow.SetActive(true);
    }

    public void Replay()
    {
        Destroy(PlayerHealthData.instance.gameObject);
        PauseUnpause.SetPause(false);
        StartCoroutine(MenuStatics.LoadScene(2));
    }
}
