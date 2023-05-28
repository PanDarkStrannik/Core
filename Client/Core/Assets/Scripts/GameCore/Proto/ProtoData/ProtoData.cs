using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore.Proto
{
    [CreateAssetMenu(fileName = "New ProtoData", menuName = "Balance/ProtoData")]
    public class ProtoData : SerializedScriptableObject
    {
        [SerializeField, OnValueChanged(nameof(OnGameObjectChanged)), AssetsOnly] 
        private GameObject _gameObject;
        
        [SerializeField, ListDrawerSettings(ListElementLabelName = nameof(BaseProtoModule.ModuleName), HideRemoveButton = true)]
        private List<BaseProtoModule> _protoModules = new List<BaseProtoModule>();

        public IReadOnlyCollection<BaseProtoModule> ProtoModules => _protoModules.ToHashSet();

        private void OnGameObjectChanged()
        {
            foreach (var module in _protoModules)
            {
                module.InitializeByGameObject(_gameObject);
            }
        }
    }
}