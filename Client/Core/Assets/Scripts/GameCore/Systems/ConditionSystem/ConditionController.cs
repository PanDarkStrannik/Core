using System;
using GameCore.Patterns;

namespace GameCore.Systems.ConditionSystem
{
    public abstract class ConditionController<TData> : DataControllerBase<TData>, IConditionController
        where TData : IConditionData
    {
        public event Action OnPassed;

        public IConditionData ConditionData => Data;
        public bool IsPassed { get; private set; }

        public void Refresh()
        {
            InternalRefresh();
            IsPassed = false;
        }

        protected void Passed()
        {
            OnPassed?.Invoke();
            IsPassed = true;
        }

        protected virtual void InternalRefresh()
        {

        }
    }

    public interface IConditionController : IController
    {
        public event Action OnPassed;

        public IConditionData ConditionData { get; }

        public bool IsPassed { get; }

        public void Refresh();
    }
}
