using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace Helpers
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

        public static async Task<T> InstantiateAsync<T>(string name, Vector3 position = new Vector3(), Quaternion rotation = new Quaternion(), Transform parent = null) where T : Component
        {
            var handle = Addressables.InstantiateAsync(name, position, rotation, parent);
            await handle.Task;

            return handle.Result.GetComponent<T>();
        }

        public static async void DestroyObject(GameObject gameObject)
        {
            await AsyncHelper.Delay();

            if(Addressables.ReleaseInstance(gameObject) == false)
            {
                Destroy(gameObject);
            }
        }
    }
}