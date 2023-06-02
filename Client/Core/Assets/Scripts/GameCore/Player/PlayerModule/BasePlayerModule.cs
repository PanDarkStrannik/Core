using System;
using GameCore.Patterns;
using Sirenix.OdinInspector;

namespace GameCore
{
    [Serializable, HideReferenceObjectPicker]
    public abstract class BasePlayerModule : IData
    {
        public string ModuleName => GetType().Name;
    }
}
