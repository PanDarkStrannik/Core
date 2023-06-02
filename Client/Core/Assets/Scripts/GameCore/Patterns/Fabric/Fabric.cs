using System;
using System.Collections.Generic;

namespace GameCore.Patterns
{
    public abstract class Fabric<TCreated, TVData> : IFabric<TCreated, TVData>
    {
        public TCreated Create(TVData data)
        {
            var pairs = GetDataCreatedPair();
            return InternalCreate(data, pairs[data.GetType()]);
        }

        protected abstract TCreated InternalCreate(TVData data, Type wantCreate);

        protected abstract Dictionary<Type, Type> GetDataCreatedPair();
    }

    public interface IFabric<out TCreated, in TVData>
    {
        public TCreated Create(TVData data);
    }
}
