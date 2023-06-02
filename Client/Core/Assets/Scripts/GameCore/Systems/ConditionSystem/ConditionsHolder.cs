using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;

namespace GameCore.Systems.ConditionSystem
{
    public class ConditionsHolder
    {
        public event Action OnConditionsPassed;

        private readonly DataControllerFabric _fabric;
        private readonly bool _needRefreshAfterPassed;
        private HashSet<IConditionController> _conditionControllers;

        public ConditionsHolder(bool needRefreshAfterPassed = true)
        {
            _fabric = new DataControllerFabric();
            _needRefreshAfterPassed = needRefreshAfterPassed;
        }

        public void AddConditions(params IConditionData[] conditions)
        {
            _conditionControllers = conditions.Select(conditionData => (IConditionController)_fabric.Create(conditionData)).ToHashSet();

            foreach (var condition in _conditionControllers)
            {
                condition.OnPassed += HandleConditionPassed;
            }

            HandleConditionPassed();
        }

        public bool IsConditionsPassed()
        {
            return _conditionControllers == null || _conditionControllers.All(condition => condition.IsPassed);
        }

        private void HandleConditionPassed()
        {
            if (IsConditionsPassed())
            {
                OnConditionsPassed?.Invoke();
            }

            if (_needRefreshAfterPassed)
            {
                var conditionsData = _conditionControllers.Select(condition => condition.ConditionData).ToArray();
                _conditionControllers.Clear();
                AddConditions(conditionsData);
            }
            else
            {
                _conditionControllers.Clear();
            }
        }
    }
}
