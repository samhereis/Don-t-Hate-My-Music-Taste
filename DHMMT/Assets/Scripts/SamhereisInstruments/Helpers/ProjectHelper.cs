#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;
using System.IO;

namespace Sripts
{
    public sealed class ProjectHelper : MonoBehaviour
    {

        [ContextMenu("DeleteAllPersistentDataPath")] public void DeleteAllPersistentDataPath()
        {
            string[] filePaths = Directory.GetFiles(Application.persistentDataPath); 
            foreach (string filePath in filePaths) File.Delete(filePath);
        }
    }
}
#endif