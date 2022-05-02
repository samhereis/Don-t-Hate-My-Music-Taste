using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Identifiers
{
    public class PlayerIdentifier : IdentifierBase
    {
        public static PlayerIdentifier instance { get; private set; }

        private void Awake()
        {
            instance = this;
        }
    }
}