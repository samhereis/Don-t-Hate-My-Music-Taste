using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace Helpers
{
    public sealed class ProjectHelper : MonoBehaviour
    {
        [SerializeField] private int _targetFPS = 120;

        [Header("FPS")]
        [SerializeField] private bool _showFPS;
        [SerializeField] private CanvasGroup _fpsCanvasGroup;
        [SerializeField] private int _updateTime = 100;
        [SerializeField] private TMPro.TextMeshProUGUI _FPSCounter;
        private int _count;

        private void Awake()
        {
            Application.targetFrameRate = _targetFPS;
        }

        private void Update()
        {
            if (_showFPS == false) return;

            _count = (int)(1f / Time.unscaledDeltaTime);
            _FPSCounter.text = _count.ToString();
        }

        [ContextMenu("DeleteAllPersistentDataPath")]
        public async void DeleteAllPersistentDataPath()
        {
            string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
            foreach (string filePath in filePaths) await AsyncHelper.Delay(() => File.Delete(filePath));

            string[] folders = Directory.GetDirectories(Application.persistentDataPath);
            foreach (string folder in folders) await AsyncHelper.Delay(() => Directory.Delete(folder));
        }

        [ContextMenu("OpenPersistentDataPath")]
        public void OpenPersistentDataPath()
        {
            Process.Start(Application.persistentDataPath);
        }
    }
}