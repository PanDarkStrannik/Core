using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Utils
{
    public class ByObjectInitialize
    {
        [SerializeField, HideInInspector]
        public GameObject InitGameObject { get; private set; }

        public void InitializeByGameObject(GameObject gameObject)
        {
            InitGameObject = gameObject;
        }

        public ValueDropdownList<T> GetDropdownFromObject<T>()
            where T : Component
        {
            var valueDropDownList = new ValueDropdownList<T>();
            foreach (var mono in InitGameObject.GetAllComponentsOfType<T>())
            {
                valueDropDownList.Add(mono.name, mono);
            }
            return valueDropDownList;
        }
    }
}
