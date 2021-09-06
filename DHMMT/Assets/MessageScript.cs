using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MessageScript : MonoBehaviour
{
    public static MessageScript instance;

    public GameObject DontHaveEnoughKillsMessage;

    public GameObject InteractMessage;

    void OnEnable()
    {
        instance = this;
    }

    void OnDisable()
    {
        instance = null;
    }

    public void ShowMessage(GameObject message, float Duration)
    {
        StartCoroutine(ShowMessageCoroutine(message, Duration));
    }

    IEnumerator ShowMessageCoroutine(GameObject message, float Duration)
    {
        if(message != null && message.gameObject.activeSelf == false)
        {
            message.gameObject.SetActive(true);

            message.transform.DOLocalMoveY(-100, 1).SetUpdate(true);

            yield return Wait.NewWaitRealTime(Duration);

            message.transform.DOLocalMoveY(100, 1).SetUpdate(true);

            message.gameObject.SetActive(false);

            StopAllCoroutines();
        }
    }
}
