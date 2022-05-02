using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAgent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Animator animator => _animator;
}
