using Helpers;
using System.Threading;
using UnityEngine;

public class LoadingWindow : MonoBehaviour
{
    // Loading window data

    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private RectTransform _loadingIcon;

    private void Awake()
    {
        _sceneLoader.onSceneStartLoading.AddListener(Open);
    }

    private CancellationTokenSource _cancellationTokenSource;

    private void OnEnable()
    {
        Rotate(_cancellationTokenSource = new CancellationTokenSource());
    }

    private void OnDisable()
    {
        _cancellationTokenSource.Cancel();

    }

    private async void Rotate(CancellationTokenSource cancellationTokenSource)
    {
        int rot = 0;
        while (!cancellationTokenSource.IsCancellationRequested)
        {
            rot -= 2;

            _loadingIcon.rotation = Quaternion.Euler(0, 0, rot);

            await ExtentionMethods.Delay(0.1f);
        }
    }

    private void Open()
    {
        gameObject.SetActive(true);
    }
}
