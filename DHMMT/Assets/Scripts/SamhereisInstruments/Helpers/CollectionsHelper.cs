using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class CollectionsHelper
    {
        public static async Task RemoveNullsAsync<T>(this List<T> list)
        {
            var listCopy = new List<T>();
            listCopy.AddRange(list);

            foreach (var item in listCopy) await AsyncHelper.Delay(() => { if (item == null) list.Remove(item); });
        }

        public static void RemoveNulls<T>(this List<T> list)
        {
            list.RemoveAll(x => x == null);
        }

        public static void SafeAdd<T>(this List<T> list, T item)
        {
            if (list.Contains(item) == false) list.Add(item);
        }

        public static void SafeRemove<T>(this List<T> list, T item)
        {
            if (list.Contains(item) == true) list.Remove(item);
        }

        public static void RemoveDuplicates<T>(this List<T> list, T item)
        {
            foreach (T itemToCheck in list)
            {
                foreach (T itemToPotentiallyRemove in list)
                {
                    bool isSameIndex = list.IndexOf(itemToPotentiallyRemove) != list.IndexOf(itemToCheck);
                    bool isEqual = itemToPotentiallyRemove.Equals(itemToCheck);

                    if (isSameIndex && isEqual) list.Remove(itemToPotentiallyRemove);
                }
            }
        }

        public static async Task RemoveDuplicatesAsync<T>(this List<T> list, T item)
        {
            foreach (T itemToCheck in list)
            {
                await AsyncHelper.Delay(async () =>
                {
                    foreach (T itemToPotentiallyRemove in list)
                    {
                        await AsyncHelper.Delay(() =>
                        {
                            bool isSameIndex = list.IndexOf(itemToPotentiallyRemove) != list.IndexOf(itemToCheck);
                            bool isEqual = itemToPotentiallyRemove.Equals(itemToCheck);

                            if (isSameIndex && isEqual) list.Remove(itemToPotentiallyRemove);
                        });
                    }
                });
            }
        }

        public static T GetRandomElement<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}