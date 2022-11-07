using Mirror;
using System;
using UnityEngine;

namespace Agents
{
    public class AnimationAgent : NetworkBehaviour
    {
        public Action<string> onAnimationCallback;
        [field: SerializeField] public Animator animator { get; private set; }

        [Command]
        public void CallCallback(string callbackName)
        {
            onAnimationCallback?.Invoke(callbackName);
        }

        [Command]
        public void PlayAnimation(int animationHash)
        {
            animator.Play(animationHash);
        }

        [Command]
        public void PlayAnimation(string animationName)
        {
            animator.Play(animationName);
        }

        public void CrossFade(int animationHash, float duration = 0.5f)
        {
            animator.CrossFade(animationHash, duration);
        }

        public void CrossFade(string animationName, float duration = 0.5f)
        {
            animator.CrossFade(animationName, duration);
        }
    }
}