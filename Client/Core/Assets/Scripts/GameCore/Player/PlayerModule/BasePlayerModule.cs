using System;
using Sirenix.OdinInspector;

namespace GameCore
{
    [Serializable, HideReferenceObjectPicker]
    public abstract class BasePlayerModule
    {
        public string ModuleName => GetType().Name;
    }
}
