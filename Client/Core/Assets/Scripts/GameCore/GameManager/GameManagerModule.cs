using System;
using GameCore.Patterns;
using Sirenix.OdinInspector;

namespace GameCore
{
    [Serializable, HideReferenceObjectPicker]
    public abstract class BaseGameManagerModule : IData
    {
        public string ModuleName => GetType().Name;
    }
}
