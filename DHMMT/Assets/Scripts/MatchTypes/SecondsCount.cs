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

    public void ChagneSeconds(int value)
    {
        _seconds = value;
        text.text = _seconds.ToString();
    }

    public void Beggin(float waitBeforeExecute)
    {
        StartCoroutine(StartCount(waitBeforeExecute));
    }

    public void BegginCountDown(float waitBeforeExecute, int BegginFrom)
    {
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
        ChagneSeconds(0);
        StopAllCoroutines();
    }

    IEnumerator StartCount(float waitBeforeExecute)
    {
        yield return Wait.NewWait(waitBeforeExecute);

        while (true)
        {
            _seconds++;
            ChagneSeconds(_seconds);
            yield return Wait.NewWait(1);
        }
    }

    IEnumerator StartnCountDown(float waitBeforeExecute, int BegginFrom)
    {
        yield return Wait.NewWait(waitBeforeExecute);

        _seconds = BegginFrom;

        while (true)
        {
            _seconds--;
            ChagneSeconds(_seconds);

            if (_seconds < 1)
            {
                PlayerHealthData.instance.GetComponent<IMatchWinable>().Win();
            }

            yield return Wait.NewWait(1);
        }
    }
}
