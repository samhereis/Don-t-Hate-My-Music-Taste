using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

[System.Serializable]
public class LightRotator : MonoBehaviour
{
    public float waitForStart = 0;

    public float RotateVal, Duration;
    public float r = 0;
    public enum Axis
    {
        X,
        Y,
        Z,
    }
    [SerializeField] Axis axis;

    void Awake()
    {

    }
    void OnEnable()
    {
        r = 0;

        switch (axis)
        {
            case (Axis.X):
                StartCoroutine(RotateX());
                break;

            case (Axis.Y):
                StartCoroutine(RotateY());
                break;

            case (Axis.Z):
                StartCoroutine(RotateZ());
                break;
        }

        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            RotateVal = RotateVal * -1;
            Debug.Log(RotateVal);
        }

        StartCoroutine(changeRotateVal());
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator RotateX()
    {
        yield return Wait.NewWait(UnityEngine.Random.Range(waitForStart, waitForStart + 3));

        Start:
        {
            yield return new WaitForSecondsRealtime(Duration);

            transform.rotation = Quaternion.Euler(r += RotateVal, 0, 0);

            if (r >= 360 - RotateVal || r < -362 || r > 362)
            {
                r = 0;
            }
        }
        goto Start;
    }
    IEnumerator RotateY()
    {
        yield return Wait.NewWait(UnityEngine.Random.Range(waitForStart, waitForStart + 3));

        Start:
        {
            yield return new WaitForSecondsRealtime(Duration);

            transform.DORotate(new Vector3(0, r += RotateVal, 0), Duration);

            if (r >= 360-RotateVal)
            {
                r = 0;
                transform.DORotate(new Vector3(0, 0, 0), Duration);
            }
        }
        goto Start;
    }
    IEnumerator RotateZ()
    {
        yield return Wait.NewWait(UnityEngine.Random.Range(waitForStart, waitForStart + 3));

        Start:
        {
            yield return new WaitForSecondsRealtime(Duration);

            transform.DORotate(new Vector3(0, 0, r += RotateVal), Duration);

            if (r >= 360 - RotateVal)
            {
                r = 0;
                transform.DORotate(new Vector3(0, 0, 0), Duration);
            }
        }
        goto Start;
    }

    IEnumerator changeRotateVal()
    {
        while (true)
        {
            yield return Wait.NewWait(UnityEngine.Random.Range(40, 90));

            RotateVal *= -1;
        }
    }
}
