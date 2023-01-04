using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class CollectionsHelper
    {
        
        #region List

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
            var listCopy = new List<T>();
            listCopy.AddRange(list);

            foreach (T itemToCheck in listCopy)
            {
                foreach (T itemToPotentiallyRemove in listCopy)
                {
                    bool isEqual = itemToPotentiallyRemove.Equals(itemToCheck);

                    if (isEqual) list.Remove(itemToPotentiallyRemove);
                }
            }
        }

        public static async Task RemoveDuplicatesAsync<T>(this List<T> list)
        {
            var listCopy = new List<T>();
            listCopy.AddRange(list);

            foreach (T itemToCheck in listCopy)
            {
                await AsyncHelper.Delay(async () =>
                {
                    foreach (T itemToPotentiallyRemove in listCopy)
                    {
                        await AsyncHelper.Delay(() =>
                        {
                            bool isEqual = itemToPotentiallyRemove.Equals(itemToCheck);

                            if (isEqual) list.Remove(itemToPotentiallyRemove);
                        });
                    }
                });
            }
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        #endregion

        #region Array

        public static T GetRandom<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        #endregion
    }
}