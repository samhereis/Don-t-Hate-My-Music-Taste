using Helpers;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Canvases
{
    public sealed class ProgressCanvas : CanvasBase
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

        public override async void Enable(float? duration = null)
        {
            onACanvasOpen?.Invoke(this);

            await AsyncHelper.Delay(500);

            base.Enable(duration);
        }

        public override void Disable(float? duration = null)
        {
            base.Disable(duration);
        }

        private void OnEnable()
        {
            SetProgress(0);
        }

        private void OnDisable()
        {
            SetProgress(0);
        }

        public override void OnACanvasOpen(CanvasBase uIWIndow)
        {

        }

        public void SetProgress(float value)
        {
            _progressSlider.value = value;
        }
    }
}