using System;
using UnityEngine;

namespace Agents
{
    public class AnimationAgent : MonoBehaviour
    {
        public Action<string> onAnimationCallback;
        [field: SerializeField] public Animator animator { get; private set; }

        public void CallCallback(string callbackName)
        {
            onAnimationCallback?.Invoke(callbackName);
        }
    }
}