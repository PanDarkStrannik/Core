using GameCore.Utils;

namespace GameCore.Proto
{
    public abstract class BaseProtoModule : ByObjectInitialize
    {
        public string ModuleName => GetType().Name;
    }
}