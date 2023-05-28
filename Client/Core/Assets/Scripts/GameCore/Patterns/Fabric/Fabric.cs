using System;
using System.Collections.Generic;

namespace GameCore.Patterns
{
    public abstract class Fabric<TCreated, TVData>
    {
        public TCreated Create(TVData data)
        {
            var pairs = GetDataCreatedPair();
            return InternalCreate(data, pairs[data.GetType()]);
        }

        protected abstract TCreated InternalCreate(TVData data, Type wantCreate);

        protected abstract Dictionary<Type, Type> GetDataCreatedPair();
    }

    public abstract class InitializerFabric<TCreated, TVData> : Fabric<TCreated, TVData>
        where TCreated : IFabricCreated 
    {
        protected override TCreated InternalCreate(TVData data, Type wantCreate)
        {
            var moduleController = (TCreated)Activator.CreateInstance(wantCreate);
            moduleController?.Initialize(data);
            return moduleController;
        }
    }

    public interface IFabricCreated
    {
        public void Initialize(object data);
    }
}
