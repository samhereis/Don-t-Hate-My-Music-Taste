using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSettingsChanger : MonoBehaviour
{
    public Material sun;
    public string SettingsName;
    public int start, end;
    public float min, mult;
    void FixedUpdate()
    {
        sun.SetFloat(SettingsName, Cals.instance.setSoundFreq(start, end, mult, min));
    }
}
