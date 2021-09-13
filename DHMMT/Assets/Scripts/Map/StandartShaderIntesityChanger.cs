using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartShaderIntesityChanger : MonoBehaviour
{
    // TODO: Refactor this script

    public Material sun;
    public string SettingsName;
    public int start, end;
    public float min, mult;
    float r, g, b;
    private void Awake()
    {
        r = sun.color.r;
        g = sun.color.g;
        b = sun.color.b;
    }
    void FixedUpdate()
    {
        sun.SetVector(SettingsName, new Color(r, g, b) * PlayingMusicData.instance.setSoundFreq(start, end, mult, min));
    }
}
