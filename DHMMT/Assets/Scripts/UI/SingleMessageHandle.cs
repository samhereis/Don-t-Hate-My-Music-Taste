using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMessageHandle : MonoBehaviour
{
    // A message to a player

    [SerializeField] private float _showTime = 5;

    private void OnEnable()
    {
        ShowMessage(_showTime);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void ShowMessage(float ShowTime)
    {

    }
}
