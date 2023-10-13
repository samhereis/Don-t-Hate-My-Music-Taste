using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class LoadingMenu : CanvasWindowBase
    {
        [Header("UI Elements")]
        [SerializeField] private Transform _loadingCircle;
        [SerializeField] private Slider _progressSlider;

        [Header("Settings")]
        [SerializeField] private float _loadingCirlceRotationSpeed = 30;

        private void Update()
        {
            if (_loadingCircle != null)
            {
                _loadingCircle.eulerAngles = new Vector3(0, 0, _loadingCircle.eulerAngles.z - _loadingCirlceRotationSpeed * Time.deltaTime);
            }
        }

        public void SetProgress(float progress)
        {
            _progressSlider.value = progress;
        }
    }
}