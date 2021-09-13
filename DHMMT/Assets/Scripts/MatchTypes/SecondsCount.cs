using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondsCount : MonoBehaviour
{
    // Controlls time in seconds HUD while gameplay

    public static SecondsCount instance;

    [SerializeField] private TextMeshProUGUI _text;

    public int Seconds;

    void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        ExtentionMethods.SetWithNullCheck(ref _text, GetComponent<TextMeshProUGUI>());
    }

    public int GetSeconds()
    {
        return Seconds;
    }
    public void IncreaseSeconds(int value)
    {
        Seconds += value;
        _text.text = Seconds.ToString();
    }

    public void DecreaseSeconds(int value)
    {
        Seconds -= value;
        _text.text = Seconds.ToString();
    }

    public void NullSeconds()
    {
        Seconds = 0;
        _text.text = Seconds.ToString();
    }


    public void Beggin(float waitBeforeExecute, int BegginFrom)
    {
        Stop();
        StartCoroutine(StartCount(waitBeforeExecute, BegginFrom));
    }

    public void BegginCountDown(float waitBeforeExecute, int BegginFrom)
    {
        Stop();
        StartCoroutine(StartnCountDown(waitBeforeExecute, BegginFrom));
    }

    public void Pause()
    {

    }

    public void UnPause()
    {

    }

    public void Stop()
    {
        NullSeconds();
        StopAllCoroutines();
    }

    IEnumerator StartCount(float waitBeforeExecute, int BegginFrom)
    {
        yield return Wait.NewWait(waitBeforeExecute);

        Seconds = BegginFrom;

        while (true)
        {
            IncreaseSeconds(1);
            yield return Wait.NewWaitRealTime(1);
        }
    }

    IEnumerator StartnCountDown(float waitBeforeExecute, int BegginFrom)
    {
        yield return Wait.NewWait(waitBeforeExecute);

        Seconds = BegginFrom;

        while (true)
        {
            DecreaseSeconds(1);

            if (Seconds < 1)
            {
                PlayerHealthData.instance.GetComponent<IMatchWinable>().Win();
            }

            yield return Wait.NewWaitRealTime(1);
        }
    }
}
