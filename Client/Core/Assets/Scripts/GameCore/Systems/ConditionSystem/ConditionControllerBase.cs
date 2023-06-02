using System;
using GameCore.Patterns;
using GameCore.Utils;

namespace GameCore.Systems.ConditionSystem
{
    public abstract class ConditionControllerBase<TData> : DataControllerBase<TData>, IConditionController
        where TData : IConditionData
    {
        public IConditionData ConditionData => Data;
        public bool IsPassed { get; private set; }

        private readonly SimpleEvent _onPassed = new SimpleEvent();

        public void SubscribeOnPassed(params SimpleEvent.Subscriber[] subscribers)
        {
            if (IsPassed)
            {
                foreach (var subscriber in subscribers)
                {
                    subscriber.Invoke();
                }
            }

            _onPassed.Subscribe(subscribers);
        }

        public void Unsubscribe(object someObject)
        {
            _onPassed.Unsubscribe(someObject);
        }

        public void Refresh()
        {
            InternalRefresh();
            IsPassed = false;
        }

        protected void Passed()
        {
            _onPassed.Invoke();
            IsPassed = true;
        }

        protected virtual void InternalRefresh()
        {

        }
    }

    public interface IConditionController : IController
    {
        public void SubscribeOnPassed(params SimpleEvent.Subscriber[] subscribers);

        public void Unsubscribe(object someObject);

        public IConditionData ConditionData { get; }

        public bool IsPassed { get; }

        public void Refresh();
    }
}
