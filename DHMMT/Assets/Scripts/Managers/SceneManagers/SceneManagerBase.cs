using DI;
using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Canvases;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class SceneManagerBase : MonoBehaviour, IDIDependent, IInitializableAsync
    {
        [SerializeField] protected BaseSettings _baseSettings = new BaseSettings();

        public virtual async Awaitable InitializeAsync()
        {
            await Awaitable.NextFrameAsync();

            _baseSettings.menus = GetComponentsInChildren<CanvasWindowBase>(true).ToList();

            StartCoroutine(Initialize_Coroutine());
        }

        private IEnumerator Initialize_Coroutine()
        {
            foreach (CanvasWindowBase menu in _baseSettings.menus)
            {
                menu?.Initialize();
            }

            (this as IDIDependent).LoadDependencies();

            yield return new WaitForSecondsRealtime(_baseSettings.openOnStartDelay);

            _baseSettings.openOnStart?.Enable();
        }

        [Serializable]
        public class BaseSettings
        {
            [Header("Components")]
            public CanvasWindowBase openOnStart;

            [Header("Settings")]
            public float openOnStartDelay = 1f;

            [Header("Debug")]
            public List<CanvasWindowBase> menus = new List<CanvasWindowBase>();
        }
    }
}