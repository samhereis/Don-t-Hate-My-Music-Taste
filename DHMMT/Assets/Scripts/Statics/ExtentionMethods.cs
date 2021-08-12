using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtentionMethods
{
    public static bool SetWithNullCheck<T>(ref T obj, T value)
    {
        obj = value;

        if(obj == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool SetWithNullCheck<T>(T obj, T value)
    {
        obj = value;
        if (obj == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
