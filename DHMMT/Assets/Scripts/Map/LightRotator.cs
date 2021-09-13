using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

[System.Serializable]
public class LightRotator : MonoBehaviour
{
    // Constantly rotates an object

    public float RotateVal, Duration;
    public float RotationValue = 0;

    public enum Axis
    {
        X,
        Y,
        Z,
    }

    [SerializeField] private Axis axis;

    private void OnEnable()
    {
        RotationValue = 0;

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

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator RotateX()
    {
        Start:
        {
            yield return new WaitForSecondsRealtime(Duration);

            transform.rotation = Quaternion.Euler(RotationValue += RotateVal, 0, 0);

            if (RotationValue >= 360 - RotateVal || RotationValue < -362 || RotationValue > 362)
            {
                RotationValue = 0;
            }
        }
        goto Start;
    }

    IEnumerator RotateY()
    {
        yield return Wait.NewWait(UnityEngine.Random.Range(0, 3));

        Start:
        {
            yield return new WaitForSecondsRealtime(Duration);

            transform.DORotate(new Vector3(0, RotationValue += RotateVal, 0), Duration);

            if (RotationValue >= 360-RotateVal)
            {
                RotationValue = 0;
                transform.DORotate(new Vector3(0, 0, 0), Duration);
            }
        }
        goto Start;
    }

    IEnumerator RotateZ()
    {
        yield return Wait.NewWait(UnityEngine.Random.Range(0, 3));

        Start:
        {
            yield return new WaitForSecondsRealtime(Duration);

            transform.DORotate(new Vector3(0, 0, RotationValue += RotateVal), Duration);

            if (RotationValue >= 360 - RotateVal)
            {
                RotationValue = 0;
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
