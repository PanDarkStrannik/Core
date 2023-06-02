using GameCore.Patterns;
using GameCore.Proto;

namespace GameCore
{
    public abstract class BaseProtoModuleController<T> : DataControllerBase<BaseProtoModule>, IProtoModuleController
        where T : BaseProtoModule
    {
        public ProtoInstance ProtoInstance { get; private set; }

        public void ProtoInstancePrepared(ProtoInstance protoInstance)
        {
            ProtoInstance = protoInstance;
            ProtoInstancePrepared();
        }

        protected void ProtoInstancePrepared()
        {

        }
    }

    public interface IProtoModuleController : IController
    {
        public void ProtoInstancePrepared(ProtoInstance protoInstance);
    }
}
