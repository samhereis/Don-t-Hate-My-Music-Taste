using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sripts
{
    public class AddressableHelper : MonoBehaviour
    {
        public static void GetAddressable<T>(string name, UnityAction<T> callback)
        {
            var handle = Addressables.LoadAssetAsync<T>(name);

            handle.Completed += (operation) => { callback?.Invoke(operation.Result); };
        }
    }
}