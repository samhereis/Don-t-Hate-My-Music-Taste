using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExitArrow : MonoBehaviour
{
    // Indicating on the Exit on "Escape from haters" map

    private bool _upped;

    void OnEnable()
    {
        ChangePosition();
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    private void ChangePosition()
    {
        if (_upped == true)
        {
            transform.DOMoveY(25, 2, false);
            _upped = false;
        }
        else
        {
            transform.DOMoveY(35, 2, false);
            _upped = true;
        }
    }
}
