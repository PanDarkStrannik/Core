using GameCore.Patterns;

namespace GameCore
{
    public abstract class BaseGameManagerModuleController<TData> : FabricCreated<TData>, IBaseGameManagerModuleController
        where TData : BaseGameManagerModule
    {
        public virtual void Refresh()
        {

        }
    }

    public interface IBaseGameManagerModuleController
    {
        public void Refresh();
    }
}
