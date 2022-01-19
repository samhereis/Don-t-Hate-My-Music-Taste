using DG.Tweening;
using UnityEngine;

public class MessageScript : MonoBehaviour
{
    // Controll messages to main player

    public GameObject DontHaveEnoughKills;
    public GameObject StayUnderTheLightAndFindTheExit;
    public GameObject YouCanBuyGunsInTheShop;

    public GameObject InteractMessage;

    public void ShowMessage(GameObject message, float Duration)
    {
        ShowMessageCoroutine(message, Duration);
    }

    private void ShowMessageCoroutine(GameObject message, float Duration)
    {
        if (message != null && message.gameObject.activeSelf == false)
        {
            message.gameObject.SetActive(true);

            message.transform.DOLocalMoveY(-100, 1).SetUpdate(true);

            message.transform.DOLocalMoveY(100, 1).SetUpdate(true);

            message.gameObject.SetActive(false);

            StopAllCoroutines();
        }
    }
}
