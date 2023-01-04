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

        public async Task OnEnd(Action callback = null)
        {
            _parent?.gameObject.SetActive(false);
            _parent?.Stop();

            _flash?.gameObject.SetActive(false);
            _flash?.Stop();

            _hit?.gameObject.SetActive(true);
            _hit?.Play();

            if (_hit != null)
            {
                if (callback != null) await AsyncHelper.DelayAndDo(_hit.main.duration, () => callback.Invoke());
            }
            else
            {
                if (callback != null) await AsyncHelper.DelayAndDo(1, () => callback.Invoke());
            }
        }
    }
}