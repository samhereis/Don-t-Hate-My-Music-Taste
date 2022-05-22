using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class DancerScript : MonoBehaviour
    {
        [SerializeField] private Animator _animatorComponent;
        [SerializeField] private AnimationClip _animation;

        private void Start()
        {
            PlayAnimation(_animation);
        }

        private void PlayAnimation(AnimationClip animation)
        {
            _animatorComponent.Play(animation.name);
        }
    }
}