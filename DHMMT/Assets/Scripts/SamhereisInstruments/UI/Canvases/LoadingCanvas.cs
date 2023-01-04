using Helpers;
using System;
using System.Threading;
using TMPro;
using Tools;
using UI.Window;
using UnityEngine;

namespace UI.Canvases
{
    public class LoadingCanvas : CanvasBase
    {
        public static LoadingCanvas instance;

        [SerializeField] private TextMeshProUGUI _loadingText;

        private void Start()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            CanvasWindowBase.onAWindowOpen += OnAWindowOpen;
        }

        protected override void OnDestroy()
        {
            CanvasWindowBase.onAWindowOpen -= OnAWindowOpen;
        }

        private void OnAWindowOpen(CanvasWindowBase obj)
        {
            Close();
        }


        public void SetText(string loadingText)
        {
            _loadingText.text = loadingText;
        }
    }
}