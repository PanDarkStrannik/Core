using GameCore.Patterns;
using GameCore.Utils;

namespace GameCore.Proto
{
    public abstract class BaseProtoModule : ByObjectInitialize, IData
    {
        public string ModuleName => GetType().Name;
    }
}