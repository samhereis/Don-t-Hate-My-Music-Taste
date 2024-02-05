﻿using Helpers;
using Sirenix.OdinInspector;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class ProjectileView : MonoBehaviour, ISelfValidator
    {
        [SerializeField] private ParticleSystem _flash;
        [SerializeField] private ParticleSystem _hit;

        public void Validate(SelfValidationResult result)
        {
            foreach (var trans in GetComponentsInChildren<Transform>(true))
            {
                trans.localScale = Vector3.one;
                trans.localPosition = Vector3.zero;
                trans.localEulerAngles = Vector3.zero;
            }
        }

        public void Init()
        {
            _hit.Stop();
            _hit.gameObject.SetActive(false);

            _flash.gameObject.SetActive(true);
            _flash.Play();
        }

        public async void OnEnd(Action callback = null)
        {
            _hit.Stop();
            _hit.gameObject.SetActive(false);

            _hit.gameObject.SetActive(true);
            _hit.Play();

            if (callback != null)
            {
                await AsyncHelper.DelayFloat(_hit.main.duration);
                callback.Invoke();
            }
        }

        public async Task OnEndAsync(Action callback = null)
        {
            _hit.Stop();
            _hit.gameObject.SetActive(false);

            _hit.gameObject.SetActive(true);
            _hit.Play();

            if (callback != null)
            {
                await AsyncHelper.DelayFloat(_hit.main.duration);
                callback.Invoke();
            }
        }
    }
}