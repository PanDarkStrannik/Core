using GameCore.GameManager;
using GameCore.Patterns;

namespace GameCore
{
    public abstract class BaseGameManagerModuleController<TData> : DataControllerBase<TData>, IBaseGameManagerModuleController
        where TData : BaseGameManagerModule
    {
        protected BaseGameManager GameManager { get; private set; }

        public void GameManagerPrepared(BaseGameManager gameManager)
        {
            GameManager = gameManager;
            GameManagerPrepared();
        }

        public virtual void Refresh()
        {

        }

        protected virtual void GameManagerPrepared()
        {

        }
    }

    public interface IBaseGameManagerModuleController : IController
    {
        public void GameManagerPrepared(BaseGameManager gameManager);
        public void Refresh();
    }
}
