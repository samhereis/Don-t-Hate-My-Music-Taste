using UnityEngine;

namespace Loggers
{
    public class SimpleLogger : MonoBehaviour, ILogger
    {
        [field: SerializeField] public bool enableLogs { get; private set; }

        [Header("Settings")]
        [SerializeField] private string _prefix = string.Empty;

        public void LogInfoToConsole(string message, Object context)
        {
            Debug.Log(_prefix + ": " + message, context);
        }

        public void LogWarningToConsole(string message, Object context)
        {
            Debug.LogWarning(_prefix + ": " + message, context);
        }

        public void LogErrorToConsole(string message, Object context)
        {
            Debug.LogError(_prefix + ": " + message, context);
        }
    }
}