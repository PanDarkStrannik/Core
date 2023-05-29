using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;
using GameCore.Utils;
using UnityEngine;

namespace GameCore.GameManager
{
    [Serializable]
    public abstract class BaseGameManager<T, TV> : MonoSingleton<TV>
        where T: BaseGameManager<T, TV>.BaseGameManageControllerFabric, new()
        where TV : BaseGameManager<T,TV>
    {
        [SerializeField] private GameManagerData _gameManagerData;

        private T _fabric = new T();

        private readonly OnceCallEvent _onInitialize = new OnceCallEvent();

        public IReadOnlyList<IBaseGameManagerModuleController> Controllers { get; private set; } = new List<IBaseGameManagerModuleController>();

        protected override void Initialize()
        {
            Controllers = _gameManagerData.Modules.ToHashSet().Select(module => _fabric.Create(module)).ToList();
            _onInitialize.Invoke();
        }

        public void SubscribeOnInitialize(OnceCallEvent.Subscriber subscriber)
        {
            _onInitialize.Subscribe(subscriber);
        }

        public TController GetController<TController>()
            where TController : IBaseGameManagerModuleController
        {
            Controllers.TryGet(out TController element);
            return element;
        }

        public abstract class BaseGameManageControllerFabric : InitializerFabric<BaseGameManagerModuleController<BaseGameManagerModule>, BaseGameManagerModule>
        {
        }
    }
}
