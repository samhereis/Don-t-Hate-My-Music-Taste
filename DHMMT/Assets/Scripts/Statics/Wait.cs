using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Wait
{
    public static WaitForSecondsRealtime NewWait(float time)
    {
        return new WaitForSecondsRealtime(time);
    }
}
