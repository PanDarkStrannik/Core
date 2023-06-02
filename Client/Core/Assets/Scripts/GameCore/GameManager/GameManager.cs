using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;
using GameCore.Utils;
using UnityEngine;

namespace GameCore.GameManager
{
    [Serializable]
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private GameManagerData _gameManagerData;

        private DataControllerFabric _fabric = new DataControllerFabric();

        private readonly SimpleEvent _onInitialize = new SimpleEvent();

        private HashSet<IBaseGameManagerModuleController> _controllers = new HashSet<IBaseGameManagerModuleController>();

        private bool _isInitialized;

        protected override void Initialize()
        {
            _controllers = _gameManagerData.Modules.Select(module => (IBaseGameManagerModuleController)_fabric.Create(module)).ToHashSet();

            foreach (var controller in _controllers)
            {
                controller.GameManagerPrepared(this);
            }

            _isInitialized = true;
            _onInitialize.Invoke();
        }

        public void SubscribeOnInitialize(params SimpleEvent.Subscriber[] subscribers)
        {
            if (_isInitialized)
            {
                foreach (var subscriber in subscribers)
                {
                    subscriber.Invoke();
                }
            }

            _onInitialize.Subscribe(subscribers);
        }

        public void Unsubscribe(object someObject)
        {
            _onInitialize.Unsubscribe(someObject);
        }

        public TController GetController<TController>()
            where TController : IBaseGameManagerModuleController
        {
            _controllers.TryGet(out TController element);
            return element;
        }
    }
}
