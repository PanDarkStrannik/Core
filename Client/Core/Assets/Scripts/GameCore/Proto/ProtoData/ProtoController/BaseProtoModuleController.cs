using GameCore.Patterns;
using GameCore.Proto;

namespace GameCore
{
    public abstract class BaseProtoModuleController<T> : FabricCreated<T>, IProtoModuleController
        where T : BaseProtoModule
    {

    }

    public interface IProtoModuleController
    {

    }
}
