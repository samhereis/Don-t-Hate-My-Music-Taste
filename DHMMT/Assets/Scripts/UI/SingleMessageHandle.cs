using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMessageHandle : MonoBehaviour
{
    // A message to a player

    [SerializeField] private float _showTime = 5;

    private void OnEnable()
    {
        StartCoroutine(ShowMessage(_showTime));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator ShowMessage(float ShowTime)
    {
        while(true)
        {
            yield return Wait.NewWaitRealTime(ShowTime);

            gameObject.SetActive(false);
        }
    }
}
