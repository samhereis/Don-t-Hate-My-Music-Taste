using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSettingsManager : MonoBehaviour
{
    private async void OnEnable()
    {
        await AsyncHelper.Delay(1);

        RenderSettings.fogEndDistance = GetRandomFogDistance();

        RenderSettings.fogColor = new Color(GetRandomColorValue(), GetRandomColorValue(), GetRandomColorValue());
    }

    private float GetRandomColorValue()
    {
        return Random.Range(0f, 1f);
    }

    private float GetRandomFogDistance()
    {
        return Random.Range(70, 200);
    }
}
