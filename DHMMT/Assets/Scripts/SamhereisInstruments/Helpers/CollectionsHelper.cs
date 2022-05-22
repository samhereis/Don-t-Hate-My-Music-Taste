using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

namespace Helpers
{
    public static class CollectionsHelper
    {
        public static async Task RemoveNullsAsync<T>(this List<T> list)
        {
            await AsyncHelper.Delay(() => list.RemoveAll(x => x == null) );
        }

        public static void RemoveNulls<T>(this List<T> list)
        {
            list.RemoveAll(x => x == null);
        }

        public static T GetRandomIndex<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}