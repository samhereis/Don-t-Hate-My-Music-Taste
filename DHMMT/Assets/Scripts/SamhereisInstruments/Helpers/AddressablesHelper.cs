using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace Samhereis.Helpers
{
    public class AddressablesHelper : MonoBehaviour
    {
        public static void LoadAndDo<T>(string name, UnityAction<T> callback)
        {
            Addressables.LoadAssetAsync<T>(name).Completed += (operation) => { callback?.Invoke(operation.Result); };
        }

        public static async Task<T> GetAssetAsync<T>(string name)
        {
            var handle = Addressables.LoadAssetAsync<T>(name);
            await handle.Task;

            return handle.Result;
        }
    }
}