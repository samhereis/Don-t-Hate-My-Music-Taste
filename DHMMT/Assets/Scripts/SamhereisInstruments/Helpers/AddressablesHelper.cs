using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Helpers
{
    public class AddressablesHelper : MonoBehaviour
    {
        public static void LoadAndDo<T>(string name, UnityAction<T> callback)
        {
            var handle = Addressables.LoadAssetAsync<T>(name);

            handle.Completed += (operation) => { callback?.Invoke(operation.Result); };
        }
    }
}