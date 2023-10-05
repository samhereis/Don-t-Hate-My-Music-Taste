using DI;
using Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Canvases;
using UnityEngine;

namespace Managers.UIManagers
{
    public class SceneManagerBase : MonoBehaviour, IDIDependent, IInitializable
    {
        [Header("Components")]
        [SerializeField] private CanvasWindowBase _openOnStart;

        [Header("Settings")]
        [SerializeField] private float _openOnStartDelay = 1f;

        [Header("Debug")]
        [SerializeField] private List<CanvasWindowBase> _menus = new List<CanvasWindowBase>();

        public void Initialize()
        {
            StartCoroutine(Initialize_Coroutine());
        }

        private IEnumerator Initialize_Coroutine()
        {
            _menus = GetComponentsInChildren<CanvasWindowBase>(true).ToList();

            foreach (CanvasWindowBase menu in _menus)
            {
                menu?.Initialize();
            }

            (this as IDIDependent).LoadDependencies();

            yield return new WaitForSecondsRealtime(_openOnStartDelay);

            _openOnStart?.Enable();
        }
    }
}