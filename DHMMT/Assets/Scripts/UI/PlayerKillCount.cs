using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerKillCount : MonoBehaviour
{
    public static PlayerKillCount instance;
    [SerializeField] TextMeshProUGUI text;
    public int KillCount { get => _killCount; set { _killCount = value; text.text = KillCount.ToString(); } }
    [SerializeField] int _killCount;

    void Awake()
    {
        instance = this;
        ExtentionMethods.SetWithNullCheck(text, GetComponent<TextMeshProUGUI>());
    }

    public int GetKillCount()
    {
        return KillCount;
    }

    public void IncreaseKillCount()
    {
        KillCount++;
    }

    public void NullKillCount()
    {
        KillCount = 0;
    }
}
