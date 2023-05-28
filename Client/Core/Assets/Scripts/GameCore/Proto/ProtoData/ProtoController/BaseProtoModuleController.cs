using GameCore.Patterns;
using GameCore.Proto;

namespace GameCore
{
    public class BaseProtoModuleController<T> : IDataDriver<T>, IProtoModuleController
        where T : BaseProtoModule
    {
        public T Data { get; private set; }
        public void Initialize(T data)
        {
            Data = data;
            InternalInitialize();
        }

        public void Initialize(object data)
        {
            Initialize(data as T);
        }

        protected virtual void InternalInitialize()
        {

        }
    }

    public interface IProtoModuleController : IFabricCreated
    {

    }
}
