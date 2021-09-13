using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intensityChanger : MonoBehaviour
{
    // Changes intencity of a material based on "PlayingMusicData" data

    public Light LightObj;
    public string SettingsName;
    public int Start, End;
    public float Min, Mult;

    void FixedUpdate()
    {
        LightObj.intensity  = PlayingMusicData.instance.setSoundFreq(Start, End, Mult, Min);
    }
}
