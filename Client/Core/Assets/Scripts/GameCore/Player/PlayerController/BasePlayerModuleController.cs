using GameCore.Patterns;

namespace GameCore
{
    public abstract class BasePlayerModuleController<T> : DataControllerBase<T>, IBasePlayerModuleController
        where T : BasePlayerModule
    {
        public virtual void Destroy()
        {

        }
    }

    public interface IBasePlayerModuleController : IController
    {
        public void Destroy();
    }
}
