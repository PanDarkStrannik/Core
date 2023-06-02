using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCore.Patterns
{
    public abstract class MonoSingleton<T> : MonoBehaviour
        where T : MonoSingleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                var instances = FindObjectsOfType<T>();
                CleanSingletons(instances);

                if (_instance != null && instances.Contains(_instance))
                {
                    return _instance;
                }

                var emptyGo = new GameObject(nameof(T), typeof(T));
                _instance = emptyGo.GetComponent<T>();

                return _instance;
            }
        }


        private void Awake()
        {
            DontDestroyOnLoad(this);
            Initialize();
        }

        protected abstract void Initialize();

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