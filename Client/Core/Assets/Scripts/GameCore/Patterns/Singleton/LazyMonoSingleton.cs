using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore.Patterns
{
    public abstract class LazyMonoSingleton<T> : MonoBehaviour
        where T : LazyMonoSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                var singletons = FindObjectsOfType<T>();
                CleanSingletons(singletons);

                return _instance;
            }
        }

        private static void CleanSingletons(IEnumerable<T> singletons)
        {
            if (!singletons.Any()) return;
            if (_instance == null) _instance = singletons.First();

            foreach (var singleton in singletons.ToArray())
            {
                if (singleton != _instance)
                    Destroy(singleton);
            }
        }
    }
}
