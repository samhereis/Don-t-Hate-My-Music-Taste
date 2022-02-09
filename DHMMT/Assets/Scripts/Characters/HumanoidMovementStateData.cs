using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Sripts
{
    public abstract class HumanoidMovementStateData : MonoBehaviour
    {
        public abstract bool isMoving { get; }

        public abstract bool isSprinting { get; }

        public Action<bool, float> onIsMovingChange;
        public Action<bool> onIsSprintingChange;
    }
}