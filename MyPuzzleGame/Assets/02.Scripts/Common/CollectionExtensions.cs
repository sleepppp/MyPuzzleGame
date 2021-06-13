using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class CollectionExtensions
    {
        public static TValue GetValue<TKey,TValue>(this Dictionary<TKey,TValue> collection, TKey key)
        {
            TValue result;
            collection.TryGetValue(key, out result);
            return result;
        }

        public static void AddUnique<T>(this List<T> collection, T value)
        {
            if (collection.Contains(value) == false)
                collection.Add(value);
        }
    }
}