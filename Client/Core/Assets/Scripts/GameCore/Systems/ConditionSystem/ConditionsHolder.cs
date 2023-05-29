using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;

namespace GameCore.Systems.ConditionSystem
{
    public class ConditionsHolder
    {
        public event Action OnConditionsPassed;

        private readonly InitializerFabric<ConditionController<IConditionData>, IConditionData> _fabric;
        private readonly bool _needRefreshAfterPassed;
        private List<ConditionController<IConditionData>> _conditionControllers;

        public ConditionsHolder(InitializerFabric<ConditionController<IConditionData>, IConditionData> fabric,
            bool needRefreshAfterPassed = true)
        {
            _fabric = fabric;
            _needRefreshAfterPassed = needRefreshAfterPassed;
        }

        public void AddConditions(params IConditionData[] conditions)
        {
            _conditionControllers = conditions.Select(condition => _fabric.Create(condition)).ToList();

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
                var conditionsData = _conditionControllers.Select(condition => condition.Data).ToArray();
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
