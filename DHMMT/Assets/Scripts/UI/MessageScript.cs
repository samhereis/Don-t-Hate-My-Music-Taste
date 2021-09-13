using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MessageScript : MonoBehaviour
{
    // Controll messages to main player

    public static MessageScript instance;

    public GameObject DontHaveEnoughKills;
    public GameObject StayUnderTheLightAndFindTheExit;
    public GameObject YouCanBuyGunsInTheShop;

    public GameObject InteractMessage;

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void ShowMessage(GameObject message, float Duration)
    {
        StartCoroutine(ShowMessageCoroutine(message, Duration));
    }

    private IEnumerator ShowMessageCoroutine(GameObject message, float Duration)
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
