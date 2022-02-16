using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;
using Events;

namespace Sripts
{
    public class OnKeyWin : MonoBehaviour
    {
        [SerializeField] private EventWithNoParameters _eventWithNoParameters;

        [SerializeField] private KeyCode _key;

        private void Update()
        {
            if(Input.GetKeyUp(_key))
            {
                _eventWithNoParameters?.Invoke();
            }
        }
    }
}