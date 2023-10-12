using Helpers;
using Interfaces;
using UnityEngine;

namespace Managers.SceneManagers
{
    public class SceneManagerBase : MonoBehaviour, IInitializableAsync, IClearableAsync
    {
        public virtual async Awaitable InitializeAsync()
        {
            await AsyncHelper.NextFrame();
        }

        public virtual async Awaitable ClearAsync()
        {
            await AsyncHelper.NextFrame();
        }
    }
}