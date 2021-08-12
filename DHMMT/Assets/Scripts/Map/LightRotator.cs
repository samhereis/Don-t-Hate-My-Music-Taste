using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

[System.Serializable]
public class LightRotator : MonoBehaviour
{
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
    }
    void OnDisable()
    {
        switch (axis)
        {
            case (Axis.X):
                StopCoroutine(RotateX());
                break;

            case (Axis.Y):
                StopCoroutine(RotateY());
                break;

            case (Axis.Z):
                StopCoroutine(RotateZ());
                break;
        }
    }
    IEnumerator RotateX()
    {
        Start:
        {
            yield return new WaitForSecondsRealtime(Duration);

            transform.rotation = Quaternion.Euler(r += RotateVal, 0, 0);

            if (r >= 360 - RotateVal)
            {
                r = 0;
            }
        }
        goto Start;
    }
    IEnumerator RotateY()
    {
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
}
