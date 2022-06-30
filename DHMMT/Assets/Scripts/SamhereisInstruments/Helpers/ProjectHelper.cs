#if UNITY_EDITOR
using System.IO;
using UnityEngine;

namespace Samhereis.Helpers
{
    public sealed class ProjectHelper : MonoBehaviour
    {
        [ContextMenu("DeleteAllPersistentDataPath")]
        public void DeleteAllPersistentDataPath()
        {
            string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
            foreach (string filePath in filePaths) File.Delete(filePath);
        }
    }
}
#endif