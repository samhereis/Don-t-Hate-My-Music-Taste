using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMenuAnimate : MonoBehaviour
{
    public RectTransform LoadingIcon;

    void OnEnable()
    {
        StartCoroutine(Rotate());
    }

    void OnDisable()
    {
        StopCoroutine(Rotate());

    }
    IEnumerator Rotate()
    {
        int rot = 0;
        while (gameObject.activeSelf)
        {
            rot -= 2;
            LoadingIcon.rotation = Quaternion.Euler(0, 0, rot);
            yield return new WaitForEndOfFrame();
        }
    }
}
