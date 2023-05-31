using System;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.Patterns
{
    public class DataControllerFabric : Fabric<IDataController<IData>, IData>
    {
        private readonly Dictionary<Type, Type> _dataCreatedPairs;
        public DataControllerFabric()
        {
            _dataCreatedPairs = CreateDataCreatedPair();
        }

        protected override IDataController<IData> InternalCreate(IData data, Type wantCreate)
        {
            var dataController = (IDataController<IData>)Activator.CreateInstance(wantCreate);
            dataController?.Initialize(data);
            return dataController;
        }

        protected override Dictionary<Type, Type> GetDataCreatedPair()
        {
            return _dataCreatedPairs;
        }

        private static Dictionary<Type, Type> CreateDataCreatedPair()
        {
            var controllerType = typeof(IDataController<>);

            return Utils.FinderUtils.TypeFinder.FindChildrenTypes(controllerType)
                .Select(GetControllerDataPair)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static KeyValuePair<Type, Type> GetControllerDataPair(Type controller)
        {
            var argDataTypes = Utils.FinderUtils.TypeFinder.FindAllGenericParametersForType(controller).ToArray();
            if (argDataTypes.Length > 1)
            {
                throw new Exception($"Controller: {controller.Name} has any data! You need remove some");
            }
            else if (argDataTypes.Length == 0)
            {
                throw new Exception($"Controller: {controller.Name} hasn't data!");
            }

            return new KeyValuePair<Type, Type>(controller, argDataTypes.First());
        }
    }
}
