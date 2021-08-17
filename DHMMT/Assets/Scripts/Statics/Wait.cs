using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Wait
{
    public static IEnumerable<WaitForSecondsRealtime> NewWaitYieldFree(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }

    public static WaitForSecondsRealtime NewWait(float time)
    {
        return new WaitForSecondsRealtime(time);
    }
}
