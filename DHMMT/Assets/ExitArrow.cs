using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExitArrow : MonoBehaviour
{
    bool upped;

    void OnEnable()
    {
        StartCoroutine(ChangePosition());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ChangePosition()
    {
        while(true)
        {
            if(upped == true)
            {
                transform.DOMoveY(25, 2, false);
                upped = false;
            }
            else
            {
                transform.DOMoveY(35, 2, false);
                upped = true;
            }

            yield return Wait.NewWait(2.2f);
        }
    }
}
