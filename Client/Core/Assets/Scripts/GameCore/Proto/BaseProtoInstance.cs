using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;
using UnityEngine;

namespace GameCore.Proto
{
    public abstract class BaseProtoInstance<T> : MonoBehaviour
        where T : BaseProtoInstance<T>.BaseProtoControllerFabric
    {
        [SerializeField] private ProtoData _protoData;

        public ProtoData ProtoData => _protoData;

        public IReadOnlyList<IProtoModuleController> Controllers { get; private set; }

        private T fabric;

        private void Awake()
        {
            Controllers = _protoData.ProtoModules.Select(e => fabric.Create(e)).ToList();
            Initialize();
        }

        protected virtual void Initialize()
        {

        }
        public abstract class BaseProtoControllerFabric : InitializerFabric<IProtoModuleController, BaseProtoModule>
        {

        }
    }
}