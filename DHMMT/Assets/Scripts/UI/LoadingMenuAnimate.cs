using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMenuAnimate : MonoBehaviour
{
    // Loading menu animation

    public RectTransform LoadingIcon;

    private void OnEnable()
    {
        StartCoroutine(Rotate());
    }

    private void OnDisable()
    {
        StopCoroutine(Rotate());

    }

    private IEnumerator Rotate()
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
