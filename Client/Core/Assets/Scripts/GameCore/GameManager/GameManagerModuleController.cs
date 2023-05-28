using GameCore.Patterns;

namespace GameCore
{
    public abstract class BaseGameManagerModuleController<TData> : IDataDriver<TData>, IBaseGameManagerModuleController
        where TData : BaseGameManagerModule
    {
        public TData Data { get; private set; }
        public void Initialize(TData data)
        {
            Data = data;
            InternalInitialize();
        }

        public void Initialize(object data)
        {
            Initialize(data as TData);
        }

        public virtual void Refresh()
        {
        }

        protected virtual void InternalInitialize()
        {
        }
    }

    public interface IBaseGameManagerModuleController : IFabricCreated
    {
        public void Refresh();
    }
}
