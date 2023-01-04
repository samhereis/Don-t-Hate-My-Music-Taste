using Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hit;
        [SerializeField] private ParticleSystem _flash;
        [SerializeField] private ParticleSystem _parent;

        private void OnValidate()
        {
            foreach (var trans in GetComponentsInChildren<Transform>(true))
            {
                trans.localScale = Vector3.one;
                trans.localPosition = Vector3.zero;
                trans.localEulerAngles = Vector3.zero;
            }
        }

        public async Task Init()
        {
            await AsyncHelper.Delay();

            _parent?.gameObject.SetActive(true);
            _parent?.Play();

            _hit?.gameObject.SetActive(false);
            _hit?.Stop();

            _flash?.gameObject.SetActive(true);
            _flash?.Play();
        }

        public async Task OnEnd()
        {
            try
            {
                _parent?.gameObject?.SetActive(false);
                _parent?.Stop();

                _flash?.gameObject?.SetActive(false);
                _flash?.Stop();

                _hit?.gameObject.SetActive(true);
                _hit?.Play();

            }
            finally
            {
                if (_hit != null)
                {
                    await AsyncHelper.Delay(_hit.main.duration);
                }
                else
                {
                    await AsyncHelper.Delay(1);
                }
            }
        }
    }
}