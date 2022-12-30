using Helpers;
using System.Threading;
using TMPro;
using Tools;
using UnityEngine;

namespace UI.Canvases
{
    public class LoadingCanvas : CanvasBase
    {
        [SerializeField] private TextMeshProUGUI _loadingText;

        public void SetText(string loadingText)
        {
            _loadingText.text = loadingText;
        }
    }
}