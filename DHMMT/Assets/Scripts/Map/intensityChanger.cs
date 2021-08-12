using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intensityChanger : MonoBehaviour
{
    public Light lightObj;
    public string SettingsName;
    public int start, end;
    public float min, mult;
    void FixedUpdate()
    {
        lightObj.intensity  = Cals.instance.setSoundFreq(start, end, mult, min);
    }
}
