using GameCore.Patterns;
using GameCore.Proto;

namespace GameCore
{
    public abstract class BaseProtoModuleController<T> : DataControllerBase<BaseProtoModule>, IProtoModuleController
        where T : BaseProtoModule
    {

    }

    public interface IProtoModuleController : IController
    {

    }
}
