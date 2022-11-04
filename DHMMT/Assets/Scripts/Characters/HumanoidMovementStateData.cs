using Mirror;
using System;
using UnityEngine;

namespace Characters.States.Data
{
    public abstract class HumanoidMovementStateData : NetworkBehaviour
    {
        public abstract bool isMoving { get; }
        public abstract bool isSprinting { get; }

        public Action<bool, float> onIsMovingChange;
        public Action<bool> onIsSprintingChange;
    }
}