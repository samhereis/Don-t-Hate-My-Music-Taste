using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    public sealed class ProgressCanvas : UICanvasBase
    {
        public static ProgressCanvas instance;

        [SerializeField] private Slider _progressSlider;

        protected override void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(this);
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            Disable();
            base.Awake();
        }

        private void OnEnable()
        {
            SetProgress(0);
        }

        private void OnDisable()
        {
            SetProgress(0);
        }

        public void SetProgress(float value)
        {
            _progressSlider.value = value;
        }
    }
}