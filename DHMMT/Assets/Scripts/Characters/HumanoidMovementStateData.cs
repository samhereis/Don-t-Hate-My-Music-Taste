using System;
using UnityEngine;

namespace Characters.States.Data
{
    public abstract class HumanoidMovementStateData : MonoBehaviour
    {
        public abstract bool isMoving { get; }
        public abstract bool isSprinting { get; }

        public Action<bool, float> onIsMovingChange;
        public Action<bool> onIsSprintingChange;
    }
}