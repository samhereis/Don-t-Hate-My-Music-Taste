using Interfaces;
using System;
using UnityEditor;
using UnityEngine;

namespace DataClasses
{
    [Serializable]
    public class AScene : IInitializable
    {
        [field: SerializeField] public string sceneCode { get; private set; }

#if UNITY_EDITOR

        [SerializeField] private SceneAsset _scene;

#endif

        public void Initialize()
        {
#if UNITY_EDITOR

            sceneCode = _scene.name;

#endif
        }
    }
}