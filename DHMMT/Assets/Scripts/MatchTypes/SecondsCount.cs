using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondsCount : MonoBehaviour
{
    public static SecondsCount instance;

    TextMeshProUGUI text;

    public int _seconds;

    void Awake()
    {
        ExtentionMethods.SetWithNullCheck(ref instance, this);
        ExtentionMethods.SetWithNullCheck(ref text, GetComponent<TextMeshProUGUI>());
    }

    public int GetSeconds()
    {
        return _seconds;
    }
    public void IncreaseSeconds(int value)
    {
        _seconds += value;
        text.text = _seconds.ToString();
    }

    public void DecreaseSeconds(int value)
    {
        _seconds -= value;
        text.text = _seconds.ToString();
    }

    public void NullSeconds()
    {
        _seconds = 0;
        text.text = _seconds.ToString();
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

        _seconds = BegginFrom;

        while (true)
        {
            IncreaseSeconds(1);
            yield return Wait.NewWaitRealTime(1);
        }
    }

    IEnumerator StartnCountDown(float waitBeforeExecute, int BegginFrom)
    {
        yield return Wait.NewWait(waitBeforeExecute);

        _seconds = BegginFrom;

        while (true)
        {
            DecreaseSeconds(1);

            if (_seconds < 1)
            {
                PlayerHealthData.instance.GetComponent<IMatchWinable>().Win();
            }

            yield return Wait.NewWaitRealTime(1);
        }
    }
}
