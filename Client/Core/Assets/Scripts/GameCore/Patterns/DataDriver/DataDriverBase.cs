using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Patterns
{
    public abstract class DataControllerBase<TData> : IDataController<TData>
        where TData : IData
    {
        public TData Data { get; private set; }

        public void Initialize(TData data)
        {
            Data = data;
            InternalInitialize();
        }

        protected abstract void InternalInitialize();
    }
}
