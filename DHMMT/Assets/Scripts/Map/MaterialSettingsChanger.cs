using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSettingsChanger : MonoBehaviour
{
    // Changes a materials property based on playing music

    public Material ChangedMaterial;
    public string SettingsName;
    public int Start, End;
    public float Min, Mult;

    private void FixedUpdate()
    {
        ChangedMaterial.SetFloat(SettingsName, PlayingMusicData.instance.setSoundFreq(Start, End, Mult, Min));
    }
}
