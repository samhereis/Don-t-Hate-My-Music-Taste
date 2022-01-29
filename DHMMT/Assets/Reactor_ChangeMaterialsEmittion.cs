using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Helpers;
using Scriptables.Holders.Music;

namespace Reactors
{
    public class Reactor_ChangeMaterialsEmittion : MonoBehaviour
    {
        [SerializeField] private Material _material;
        [SerializeField] private string _property;

        [SerializeField] private AFrequancyData _aFrequancyData;

        private Task _task;

        private void OnValidate()
        {
            _material.SetFloat(_property, _aFrequancyData.value * Random.Range(1, 200));
        }

        private void Awake()
        {
            _task = React();
        }

        private void OnDestroy()
        {
            _task.Dispose();
        }

        private async Task React()
        {
            while(enabled)
            {
                await AsyncHelper.Delay(Time.deltaTime);

                _material.SetFloat(_property, _aFrequancyData.value);
            }
        }
    }
}