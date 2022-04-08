using Helpers;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerKillCount : MonoBehaviour
{
    // Show player kills number while gameplay

    [SerializeField] private TextMeshProUGUI _text;

    public int KillCount { get => _killCount; set { _killCount = value; _text.text = $"ㄙ {KillCount}"; TweeningHelper.NormalShake(transform, 2); } }
    [SerializeField] private int _killCount;

    private void Awake()
    {
        _text ??= GetComponent<TextMeshProUGUI>();

        _text.text = KillCount.ToString();
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

    public void SetInfinity()
    {
        _killCount = 9999;
        _text.text = "∞";
    }
}
