using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;
using GameCore.Utils;
using UnityEngine;

namespace GameCore.GameManager
{
    [Serializable]
    public abstract class BaseGameManager : MonoSingleton<BaseGameManager>
    {
        [SerializeField] private GameManagerData _gameManagerData;

        private DataControllerFabric _fabric = new DataControllerFabric();

        private readonly OnceCallEvent _onInitialize = new OnceCallEvent();

        private HashSet<IBaseGameManagerModuleController> _controllers = new HashSet<IBaseGameManagerModuleController>();

        protected override void Initialize()
        {
            _controllers = _gameManagerData.Modules.Select(module => (IBaseGameManagerModuleController)_fabric.Create(module)).ToHashSet();

            foreach (var controller in _controllers)
            {
                controller.GameManagerPrepared(this);
            }

            _onInitialize.Invoke();
        }

        public void SubscribeOnInitialize(OnceCallEvent.Subscriber subscriber)
        {
            _onInitialize.Subscribe(subscriber);
        }

        public TController GetController<TController>()
            where TController : IBaseGameManagerModuleController
        {
            _controllers.TryGet(out TController element);
            return element;
        }
    }
}
