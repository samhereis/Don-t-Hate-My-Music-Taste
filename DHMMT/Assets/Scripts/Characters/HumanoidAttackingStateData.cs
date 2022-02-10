using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.Events;
using Helpers;

namespace Characters.States.Data
{
    public class HumanoidAttackingStateData : MonoBehaviour
    {
        public Action<bool> onAttack;
    }
}