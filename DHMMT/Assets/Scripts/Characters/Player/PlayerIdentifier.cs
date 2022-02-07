using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Identifiers
{
    public class PlayerIdentifier : MonoBehaviour
    {
        public static PlayerIdentifier instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

#if UNITY_EDITOR
        [ContextMenu("Setup")] public void Setup()
        {

        }
#endif
    }
}