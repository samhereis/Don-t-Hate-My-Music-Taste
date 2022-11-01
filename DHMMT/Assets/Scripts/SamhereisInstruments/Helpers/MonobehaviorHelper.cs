using UnityEngine;

namespace Helpers
{
    public static class MonobehaviorHelper
    {
        public static async void TrySetDirty(this MonoBehaviour monoBehaviour)
        {
#if UNITY_EDITOR
            await AsyncHelper.Delay(() => { UnityEditor.EditorUtility.SetDirty(monoBehaviour); });
#endif
        }

        public static async void TrySetDirty(this ScriptableObject scriptableObject)
        {
#if UNITY_EDITOR
            await AsyncHelper.Delay(() => { UnityEditor.EditorUtility.SetDirty(scriptableObject); });
#endif
        }
    }
}