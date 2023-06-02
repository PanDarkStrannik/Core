using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;
using UnityEngine;

namespace GameCore.Proto
{
    public abstract class BaseProtoInstance : MonoBehaviour
    {
        [SerializeField] private ProtoData _protoData;

        public ProtoData ProtoData => _protoData;

        private HashSet<IProtoModuleController> _protoModuleControllers = new HashSet<IProtoModuleController>(); 

        private readonly DataControllerFabric _fabric = new DataControllerFabric();

        private void Awake()
        {
            _protoModuleControllers = _protoData.ProtoModules
                .Select(e => (IProtoModuleController)_fabric.Create(e))
                .ToHashSet();
        }
    }
}