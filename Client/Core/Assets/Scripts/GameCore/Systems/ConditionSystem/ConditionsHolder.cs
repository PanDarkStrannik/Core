using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Patterns;
using GameCore.Utils;

namespace GameCore.Systems.ConditionSystem
{
    public class ConditionsHolder
    {
        private readonly SimpleEvent _onConditionsPassed = new SimpleEvent();
        private readonly DataControllerFabric _fabric = new DataControllerFabric();
        private readonly Dictionary<Type, IConditionController> _conditionDataTypeControllerPair = new Dictionary<Type, IConditionController>();
        private readonly HashSet<IConditionController> _conditionControllers = new HashSet<IConditionController>();

        public void SubscribeOnConditions(params SimpleEvent.Subscriber[] subscribers)
        {
            if (IsConditionsPassed())
            {
                foreach (var subscriber in subscribers)
                {
                    subscriber.Invoke();
                }
            }

            _onConditionsPassed.Subscribe(subscribers);
        }

        public void UnsubscribeFromConditions(object someObject)
        {
            _onConditionsPassed.Unsubscribe(someObject);
        }

        public void AddConditions(params IConditionData[] conditions)
        {
            var createdConditions = new HashSet<IConditionController>();
            foreach (var conditionData in conditions)
            {
                var conditionController = (IConditionController)_fabric.Create(conditionData);
                if (_conditionControllers.Contains(conditionController))
                    continue;

                createdConditions.Add(conditionController);
                _conditionControllers.Add(conditionController);
                _conditionDataTypeControllerPair.Add(conditionData.GetType(), conditionController);
                _conditionDataTypeControllerPair.Add(conditionController.GetType(), conditionController);
            }

            foreach (var condition in createdConditions)
            {
                condition.SubscribeOnPassed(HandleConditionPassed);
            }

            HandleConditionPassed();
        }

        public void RemoveConditions(params Type[] conditions)
        {
            var toRemove = new List<IConditionController>();
            var toRemoveInDict = new List<Type>();

            foreach (var conditionType in conditions)
            {
                if (!_conditionDataTypeControllerPair.TryGetValue(conditionType, out var conditionController))
                    continue;

                toRemoveInDict.Add(conditionType);
                toRemove.Add(conditionController);
            }

            foreach (var condition in toRemove)
            {
                condition.Unsubscribe(this);
                _conditionControllers.Remove(condition);
            }

            foreach (var someType in toRemoveInDict)
            {
                _conditionDataTypeControllerPair.Remove(someType);
            }
        }

        private bool IsConditionsPassed()
        {
            return _conditionControllers == null || _conditionControllers.All(condition => condition.IsPassed);
        }

        private void HandleConditionPassed()
        {
            if (!IsConditionsPassed())
                return;

            _onConditionsPassed.Invoke();

            _conditionDataTypeControllerPair.Clear();

            var conditionsToRestart = _conditionControllers
                .Select(condition => condition.ConditionData)
                .Where(conditionData => conditionData.NeedRefreshAfterPassed)
                .ToArray();

            _conditionControllers.Clear();

            AddConditions(conditionsToRestart);
        }
    }
}
