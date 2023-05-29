using System;
using GameCore.Patterns;

namespace GameCore.Systems.ConditionSystem
{
    public abstract class ConditionController<TData> : FabricCreated<TData>, IConditionController
        where TData : IConditionData
    {
        private bool _isPassed;
        public event Action OnPassed;

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

    public interface IConditionController
    {
        public event Action OnPassed;

        public bool IsPassed { get; }

        public void Refresh();
    }
}
