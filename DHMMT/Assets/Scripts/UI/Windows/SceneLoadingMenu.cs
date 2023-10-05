using UI.Canvases;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class SceneLoadingMenu : CanvasWindowBase
    {
        [Header("UI Elements")]
        [SerializeField] private Slider _progressSlider;

        public void SetProgress(float progress)
        {
            _progressSlider.value = progress;
        }
    }
}