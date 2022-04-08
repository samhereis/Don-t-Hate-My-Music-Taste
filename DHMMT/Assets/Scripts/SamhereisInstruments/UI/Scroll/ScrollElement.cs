using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Sripts
{
    public sealed class ScrollElement : MonoBehaviour
    {
        [Header("Componenets")]
        [SerializeField] private GameObject _gameObject;

        [Header("Settings")]
        [SerializeField] private float _setting;

        [Header("Debug")]
        [SerializeField] private float _debug;

        private void OnValidate()
        {
            
        }

        private void Awake()
        {
            
        }

        public void Enable()
        {
            
        }

        public void Disable()
        {
            
        }

	    private void OnDestroy()
        {
            
        }

#if UNITY_EDITOR
        [ContextMenu("Setup")] public void Setup()
        {

        }
#endif
    }
}