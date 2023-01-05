using System;
using Photon.Pun;
using UnityEngine;

namespace Agents
{
    public class AnimationAgent : MonoBehaviourPunCallbacks
    {
        public Action<string> onAnimationCallback;
        [field: SerializeField] public Animator animator { get; private set; }

        #region Client

        public void CallCallback(string callbackName)
        {
            onAnimationCallback?.Invoke(callbackName);
        }

        public void PlayAnimation(int animationHash)
        {
            animator.Play(animationHash);
        }

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

        public void SetFloat(string floatName, float value)
        {
            animator.SetFloat(floatName, value);
        }

        public void SetFloat(int hash, float value)
        {
            animator.SetFloat(hash, value);
        }

        public void SetBool(string floatName, bool value)
        {
            animator.SetBool(floatName, value);
        }

        #endregion

        #region  Server

        [PunRPC]
        public void RPC_CallCallback(string callbackName)
        {
            CallCallback(callbackName);
        }

        [PunRPC]
        public void RPC_PlayAnimation(int animationHash)
        {
            PlayAnimation(animationHash);
        }

        [PunRPC]
        public void RPC_PlayAnimation(string animationName)
        {
            PlayAnimation(animationName);
        }

        [PunRPC]
        public void RPC_CrossFade(int animationHash, float duration = 0.5f)
        {
            CrossFade(animationHash, duration);
        }

        [PunRPC]
        public void RPC_CrossFade(string animationName, float duration = 0.5f)
        {
            CrossFade(animationName, duration);
        }

        [PunRPC]
        public void RPC_SetFloat(string floatName, float value)
        {
            SetFloat(floatName, value);
        }

        [PunRPC]
        public void RPC_SetFloat(int hash, float value)
        {
            SetFloat(hash, value);
        }

        [PunRPC]
        public void RPC_SetBool(string floatName, bool value)
        {
            SetBool(floatName, value);
        }

        #endregion
    }
}