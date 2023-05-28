using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Utils
{
    public static class MonoExt
    {
        public static IEnumerable<T> GetAllComponentsOfType<T>(this GameObject gameObject)
            where T : Component
        {
            var allList = new List<T>();
            allList.AddRange(gameObject.GetComponents<T>());

            for (var i = 0; i < gameObject.transform.childCount; i++)
            {
                var child = gameObject.transform.GetChild(i).gameObject;
                allList.AddRange(child.GetAllComponentsOfType<T>());
            }

            return allList;
        }
    }
}
