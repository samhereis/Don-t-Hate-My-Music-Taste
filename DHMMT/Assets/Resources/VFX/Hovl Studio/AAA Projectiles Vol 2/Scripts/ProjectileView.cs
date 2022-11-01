using Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private GameObject hit;
        [SerializeField] private GameObject flash;
        [SerializeField] private GameObject[] Detached;

        [SerializeField] private ParticleSystem _hit;
        [SerializeField] private ParticleSystem _flash;
        [SerializeField] private List<ParticleSystem> _detached;
        [SerializeField] private ParticleSystem _parent;

        private void OnValidate()
        {
            if (_flash == null) _flash = Instantiate(flash, transform.position, Quaternion.identity, transform).GetComponent<ParticleSystem>();
            if (_hit == null) _hit = Instantiate(hit, transform.position, Quaternion.identity, transform).GetComponent<ParticleSystem>();

            foreach (var trans in GetComponentsInChildren<Transform>(true))
            {
                trans.localScale = Vector3.one;
                trans.localPosition = Vector3.zero;
                trans.localEulerAngles = Vector3.zero;
            }

            _detached.Clear();

            foreach (var p in Detached)
            {
                if (_detached.Contains(p.GetComponent<ParticleSystem>()) == false) _detached.Add(p.GetComponent<ParticleSystem>());
            }

            if (_parent == null)
            {
                if (transform.Find("Parent") != null) _parent = transform.Find("Parent").GetComponent<ParticleSystem>();
                else Debug.LogError(gameObject.name + "  doesnt have a PARENT");
            }
        }

        public async Task Init()
        {
            _parent.gameObject.SetActive(true);
            _parent.Play();

            foreach (var partilce in _detached) await AsyncHelper.Delay(() => partilce.Play());

            if (_flash == null) _flash = Instantiate(flash, transform.position, Quaternion.identity, transform).GetComponent<ParticleSystem>(); ;
            if (_hit == null) _hit = Instantiate(hit, transform.position, Quaternion.identity, transform).GetComponent<ParticleSystem>();

            _hit.gameObject.SetActive(false);
            _hit.Stop();

            _flash.gameObject.SetActive(true);
            _flash.Play();
        }

        public async Task OnEnd(Action callback = null)
        {
            _parent?.gameObject.SetActive(false);
            _parent.Stop();

            foreach (var partilce in _detached) await AsyncHelper.Delay(() => partilce.Stop());

            _flash.gameObject.SetActive(false);
            _flash.Stop();

            _hit.gameObject.SetActive(true);
            _hit.Play();

            if (callback != null) await AsyncHelper.DelayAndDo(_hit.main.duration, () => callback.Invoke());
        }
    }
}