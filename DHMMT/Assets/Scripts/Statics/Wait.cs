using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Wait
{
    // Used for couroutines

    public static WaitForSeconds NewWait(float time)
    {
        return new WaitForSeconds(time);
    }

    public static WaitForSecondsRealtime NewWaitRealTime(float time)
    {
        return new WaitForSecondsRealtime(time);
    }
}
