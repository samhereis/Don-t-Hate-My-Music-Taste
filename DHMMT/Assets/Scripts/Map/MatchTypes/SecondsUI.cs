using System.Collections;
using TMPro;
using UnityEngine;

public class SecondsUI : MonoBehaviour
{
    // Controlls time in seconds HUD while gameplay

    public static SecondsUI instance;

    [SerializeField] private TextMeshProUGUI _text;

    public int Seconds;

    void Awake()
    {
        instance ??= this;

        _text ??= GetComponent<TextMeshProUGUI>();
    }

    public int GetSeconds()
    {
        return Seconds;
    }
    
}
