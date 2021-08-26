using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Wait
{
    public static WaitForSeconds NewWait(float time)
    {
        return new WaitForSeconds(time);
    }
}
