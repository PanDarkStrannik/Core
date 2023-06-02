using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;
using GameCore.Utils;
using UnityEngine;

namespace GameCore.Proto
{
    [Serializable]
    public class ProtoInstance : MonoBehaviour
    {
        [SerializeField] private ProtoData _protoData;

        public ProtoData ProtoData => _protoData;

        private HashSet<IProtoModuleController> _protoModuleControllers = new HashSet<IProtoModuleController>(); 

        private readonly DataControllerFabric _fabric = new DataControllerFabric();

        private void Awake()
        {
            _protoModuleControllers = _protoData.ProtoModules
                .Select(e => (IProtoModuleController) _fabric.Create(e))
                .ToHashSet();

            foreach (var controller in _protoModuleControllers)
            {
                controller.ProtoInstancePrepared(this);
            }
        }

        public TController GetController<TController>()
            where TController : IProtoModuleController
        {
            _protoModuleControllers.TryGet(out TController controller);
            return controller;
        }
    }
}