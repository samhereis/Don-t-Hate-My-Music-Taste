#if UNITY_EDITOR
using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Sripts
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