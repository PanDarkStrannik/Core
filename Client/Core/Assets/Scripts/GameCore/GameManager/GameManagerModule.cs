using System;
using Sirenix.OdinInspector;

namespace GameCore
{
    [Serializable, HideReferenceObjectPicker]
    public abstract class BaseGameManagerModule
    {
        public string ModuleName => GetType().Name;
    }
}
