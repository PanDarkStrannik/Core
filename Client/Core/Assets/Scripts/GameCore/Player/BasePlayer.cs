using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;
using GameCore.Utils;
using UnityEngine;

namespace GameCore
{
    public abstract class BasePlayer : LazyMonoSingleton<BasePlayer>
    {
        [SerializeField] private PlayerData _playerData;

        private readonly DataControllerFabric _fabric = new DataControllerFabric();
        private readonly OnceCallEvent _onInitialize = new OnceCallEvent();

        private HashSet<IBasePlayerModuleController> _moduleControllers = new HashSet<IBasePlayerModuleController>();

        private void Awake()
        {
            _moduleControllers = _playerData.Modules.Select(e => (IBasePlayerModuleController)_fabric.Create(e)).ToHashSet();
            InternalInitialize();
            _onInitialize.Invoke();
        }

        protected virtual void InternalInitialize()
        {

        }

        public void SubscribeOnInitialize(OnceCallEvent.Subscriber subscriber)
        {
            _onInitialize.Subscribe(subscriber);
        }

        private void OnDestroy()
        {
            foreach (var controller in _moduleControllers)
            {
                controller.Destroy();
            }
        }

        public TController GetController<TController>()
            where TController : IBasePlayerModuleController
        {
            _moduleControllers.TryGet(out TController element);
            return element;
        }
    }
}
