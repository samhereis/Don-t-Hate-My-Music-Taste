using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;
using Events;

namespace UI.Window
{

    public sealed class WinWindow : UIWIndowBase
    {
        [Header("SO")]
        [SerializeField] private EventWithNoParameters _eventWithNoParameters;

        private void Awake()
        {
            _eventWithNoParameters.AdListener(Enable);
        }

#if UNITY_EDITOR
        public override void OnAWindowOpen(UIWIndowBase uIWIndow)
        {

        }

        public override void Enable()
        {
            _eventWithNoParameters.RemoveListener(Enable);
            _windowBehavior.Open();
            onAWindowOpen?.Invoke(this);
        }

        public override void Setup()
        {

        }
#endif
    }
}