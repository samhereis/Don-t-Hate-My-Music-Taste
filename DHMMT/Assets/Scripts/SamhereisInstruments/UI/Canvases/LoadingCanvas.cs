using Helpers;
using System.Threading;
using TMPro;
using Tools;
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
        }

        public void SetText(string loadingText)
        {
            _loadingText.text = loadingText;
        }
    }
}