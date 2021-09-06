using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerKillCount : MonoBehaviour
{
    public static PlayerKillCount instance;
    [SerializeField] TextMeshProUGUI text;
    public int KillCount { get => _killCount; set { _killCount = value; text.text = $"ㄙ {KillCount}"; AnimationStatics.NormalShake(transform, 2); } }
    [SerializeField] int _killCount;

    void Awake()
    {
        instance = this;
        ExtentionMethods.SetWithNullCheck(text, GetComponent<TextMeshProUGUI>());
        text.text = KillCount.ToString();
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

    public void StartCheckingForEnemies(int time)
    {
        StartCoroutine(ConstanclyCheckEnemies(time));
    }

    IEnumerator ConstanclyCheckEnemies(int time)
    {
        while(true)
        {
            KillCount = Spawner.instance.enemies.Count + Spawner.instance.enemiesReserve.Count;

            yield return Wait.NewWaitRealTime(time);
        }
    }
}
