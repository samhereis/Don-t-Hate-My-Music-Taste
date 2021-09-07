using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMessageHandle : MonoBehaviour
{
    [SerializeField] float ShowTime = 5;

    void OnEnable()
    {
        StartCoroutine(ShowMessage(ShowTime));
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ShowMessage(float ShowTime)
    {
        while(true)
        {
            yield return Wait.NewWaitRealTime(ShowTime);

            gameObject.SetActive(false);
        }
    }
}
