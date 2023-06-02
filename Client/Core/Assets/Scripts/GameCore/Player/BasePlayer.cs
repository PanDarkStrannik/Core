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
        private readonly SimpleEvent _onInitialize = new SimpleEvent();

        private HashSet<IBasePlayerModuleController> _moduleControllers = new HashSet<IBasePlayerModuleController>();

        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            _moduleControllers = _playerData.Modules.Select(e => (IBasePlayerModuleController)_fabric.Create(e)).ToHashSet();
            InternalInitialize();
            IsInitialized = true;
            _onInitialize.Invoke();
        }

        protected virtual void InternalInitialize()
        {

        }

        public void SubscribeOnInitialize(SimpleEvent.Subscriber subscriber)
        {
            if (IsInitialized)
            {
                subscriber.Invoke();
            }
            _onInitialize.Subscribe(subscriber);
        }

        private void OnDestroy()
        {
            foreach (var controller in _moduleControllers)
            {
                controller.Destroy();
            }

            _onInitialize.Clear();
            IsInitialized = false;
        }

        public TController GetController<TController>()
            where TController : IBasePlayerModuleController
        {
            _moduleControllers.TryGet(out TController element);
            return element;
        }
    }
}
