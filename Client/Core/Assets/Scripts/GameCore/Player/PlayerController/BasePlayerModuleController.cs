using GameCore.Patterns;

namespace GameCore
{
    public abstract class BasePlayerModuleController<T> : FabricCreated<T>, IBasePlayerModuleController
        where T : BasePlayerModule
    {
        public virtual void Destroy()
        {

        }
    }

    public interface IBasePlayerModuleController
    {
        public void Destroy();
    }
}
