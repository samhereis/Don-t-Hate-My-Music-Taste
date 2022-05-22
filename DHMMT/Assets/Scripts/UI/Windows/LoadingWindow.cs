using Helpers;
using System.Threading;
using UnityEngine;

namespace UI
{
    public class LoadingWindow : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private RectTransform _loadingIcon;

        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _sceneLoader.onSceneStartLoading.AddListener(Open);
        }

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
                await AsyncHelper.Delay(0.1f);
            }
        }

        private void Open()
        {
            gameObject.SetActive(true);
        }
    }
}