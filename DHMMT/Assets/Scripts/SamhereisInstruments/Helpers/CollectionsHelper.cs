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
            List<T> listTemp = new List<T>();
            listTemp.AddRange(list);

            foreach (var item in listTemp)
            {
                await AsyncHelper.Delay();

                if (item == null)
                {
                    list.Remove(item);
                }
            }
        }

        public static T GetRandomIndex<T>(this List<T> list, int maxValue)
        {
            return list[Random.Range(0, maxValue)];
        }
    }
}