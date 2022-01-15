using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerKillCount : MonoBehaviour
{
    // Show player kills number while gameplay

    public static PlayerKillCount instance;

    [SerializeField] private TextMeshProUGUI _text;

    public int KillCount { get => _killCount; set { _killCount = value; _text.text = $"ㄙ {KillCount}"; AnimationStatics.NormalShake(transform, 2); } }
    [SerializeField] private int _killCount;

    private void Awake()
    {
        instance = this;
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

    public void StartCheckingForEnemies(int time)
    {
        StartCoroutine(ConstanclyCheckEnemies(time));
    }

    private IEnumerator ConstanclyCheckEnemies(int time)
    {
        while(true)
        {
            KillCount = Spawner.instance.Enemies.Count + Spawner.instance.EnemiesReserve.Count;

            yield return Wait.NewWaitRealTime(time);
        }
    }
}
