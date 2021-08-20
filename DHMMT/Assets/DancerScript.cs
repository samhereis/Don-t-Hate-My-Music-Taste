using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerScript : MonoBehaviour
{
    public Animator animator;
    public AnimationClip animationClip;

    void Start()
    {
        PlayAnimation(animationClip);
    }
    void PlayAnimation(AnimationClip animation)
    {
        animator.Play(animation.name);
    }
}
