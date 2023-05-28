using GameCore.Patterns;

namespace GameCore
{
    public abstract class BasePlayerModuleController<T> : IDataDriver<T>, IBasePlayerModuleController
        where T : BasePlayerModule
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

        public virtual void Destroy()
        {

        }
    }

    public interface IBasePlayerModuleController : IFabricCreated
    {
        public void Destroy();
    }
}
