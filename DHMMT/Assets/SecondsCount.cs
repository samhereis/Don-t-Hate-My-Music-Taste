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

    IEnumerator StartCount(float waitBeforeExecute)
    {
        yield return Wait.NewWait(waitBeforeExecute);

        while(true)
        {
            _seconds++;
            text.text = _seconds.ToString();
            yield return Wait.NewWait(1);
        }
    }

    public void Beggin(float waitBeforeExecute)
    {
        StartCoroutine(StartCount(waitBeforeExecute));
    }

    public void Pause()
    {

    }

    public void UnPause()
    {

    }

    public void Stop()
    {
        _seconds = 0;
        text.text = _seconds.ToString();
        StopAllCoroutines();
    }
}
