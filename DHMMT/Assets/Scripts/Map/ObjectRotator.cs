using DG.Tweening;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class ObjectRotator : MonoBehaviour
{
    // Constantly rotates an object

    [Header("Settings")]
    private float _directionValue;
    [SerializeField] private float _duration = 1;
    [SerializeField] private float _rotationValue = 0;

    [Header("Direction")]
    [SerializeField] private Axis _axis;

    private enum Axis { X, Y, Z, }

    private CancellationTokenSource _cancellationTokenSource;

    private void OnEnable()
    {
        _rotationValue = 0;

        switch (_axis)
        {
            case (Axis.X):
                RotateX(_cancellationTokenSource = new CancellationTokenSource());
                break;

            case (Axis.Y):
                RotateY(_cancellationTokenSource = new CancellationTokenSource());
                break;

            case (Axis.Z):
                RotateZ(_cancellationTokenSource = new CancellationTokenSource());
                break;
        }

        if (UnityEngine.Random.Range(0, 2) == 1) _directionValue = _directionValue * -1;

        ChangeRotateVal(_cancellationTokenSource = new CancellationTokenSource());
    }

    private void OnDisable()
    {
        _cancellationTokenSource.Cancel();

        transform.DOKill();
    }

    private void RotateX(CancellationTokenSource cancellationTokenSource)
    {
        Rotate(cancellationTokenSource, Vector3.right);
    }

    private void RotateY(CancellationTokenSource cancellationTokenSource)
    {
        Rotate(cancellationTokenSource, Vector3.up);
    }

    private void RotateZ(CancellationTokenSource cancellationTokenSource)
    {
        Rotate(cancellationTokenSource, Vector3.forward);
    }

    private async void Rotate(CancellationTokenSource cancellationTokenSource, Vector3 axis)
    {
        while (!cancellationTokenSource.IsCancellationRequested && gameObject.activeInHierarchy)
        {
            await ExtentionMethods.Delay(_duration);

            transform.DORotate(axis * (_rotationValue += _directionValue), _duration);

            if (_rotationValue >= 360 - _directionValue)
            {
                _rotationValue = 0;
                transform.DORotate(new Vector3(0, 0, 0), _duration);
            }
        }
    }

    private async void ChangeRotateVal(CancellationTokenSource cancellationTokenSource)
    {
        while (!cancellationTokenSource.IsCancellationRequested && gameObject.activeInHierarchy)
        {
            await ExtentionMethods.Delay(UnityEngine.Random.Range(40, 90));

            _directionValue *= -1;
        }
    }
}
