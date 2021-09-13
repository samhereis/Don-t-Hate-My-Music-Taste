using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancerScript : MonoBehaviour
{
    // Controlld a dancers animation

    public Animator AnimatorComponent;
    public AnimationClip Animation;

    private void Start()
    {
        PlayAnimation(Animation);
    }

    private void PlayAnimation(AnimationClip animation)
    {
        AnimatorComponent.Play(animation.name);
    }
}
